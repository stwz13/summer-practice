using System.Reflection;
using CommandLib;
namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            string currDirectory = Directory.GetCurrentDirectory();
            string solutionDirectory = Path.GetFullPath(Path.Combine(currDirectory, "..", "..", "..", ".."));
            string searchDllFile = Directory.GetFiles(solutionDirectory, "FileSystemCommands.dll", SearchOption.AllDirectories)[0];

            Assembly assembly = Assembly.LoadFrom(searchDllFile);

            Type typeDirectorySize = assembly.GetType("FileSystemCommands.DirectorySizeCommand")!;
            ICommand directorySizeInstance = (ICommand)Activator.CreateInstance(typeDirectorySize, args[0])!;

            directorySizeInstance.Execute();

            Type typeSearchFiles = assembly.GetType("FileSystemCommands.FindFilesCommand")!;
            ICommand searchInstance = (ICommand)Activator.CreateInstance(typeSearchFiles, Directory.GetCurrentDirectory(), args[1])!;

            searchInstance.Execute();

        }
    }
}
