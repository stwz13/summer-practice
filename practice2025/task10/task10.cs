using PluginLib;
using System.Reflection;


namespace task10
{
    public class ExecuteTheSystemOfPlugins
    {
        public string LibrariesDirect {  get; private set; }

        public Dictionary<Type, Type[]> DependenciesOfPlagins { get; private set; }
        public List<Type> SortedPlugins { get; private set; }

        public ExecuteTheSystemOfPlugins(string librariesDirect)
        {
            LibrariesDirect = librariesDirect;

            var loadedAssemblies = new HashSet<string>();

            var typiedInLibraries = Directory.GetFiles(LibrariesDirect, "*.dll", SearchOption.AllDirectories)
                .Where(assemb => loadedAssemblies.Add(AssemblyName.GetAssemblyName(assemb).FullName))
                .Select(lib => Assembly.LoadFrom(lib))
                .SelectMany(lib => lib.GetTypes());


            var commandsWithPlaginLoad = typiedInLibraries.Where(type => type.GetCustomAttribute<PluginLoadAttribute>() != null);


            DependenciesOfPlagins = commandsWithPlaginLoad
                .ToDictionary(type => type,
                type => type.GetCustomAttribute<PluginLoadAttribute>()!.PlaginsDepends);

            SortedPlugins = SortGrafOfPlagins(DependenciesOfPlagins);

            SortedPlugins.Select(command => (IPlugin)Activator.CreateInstance(command)!).ToList().ForEach(plugin => plugin.Execute());
        }

        public static List<Type> SortGrafOfPlagins(Dictionary<Type, Type[]> grafOfDepend)
        {
            var grafCopy = new Dictionary<Type, Type[]>(grafOfDepend);

            List<Type> sortedVertex = new List<Type>();

            while (grafCopy.Count > 0)
            {
                List<Type> independentVertexes = grafCopy.Where(vertex => vertex.Value.Length == 0).Select(vertex => vertex.Key).ToList();

                sortedVertex.AddRange(independentVertexes);

                grafCopy = grafCopy.Where(node => !independentVertexes.Contains(node.Key))
                    .ToDictionary(node => node.Key,
                    node => node.Value.Where(vertex => !independentVertexes.Contains(vertex)).ToArray());

            }
            return sortedVertex;

        }

    }
}