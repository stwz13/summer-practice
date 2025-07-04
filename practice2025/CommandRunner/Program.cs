using System.Reflection;
using CommandLib;
namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {

            string searchDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileSystemCommands.dll");
            Assembly assembly = Assembly.LoadFrom(searchDllPath);

            Type typeDirectorySize = assembly.GetType("FileSystemCommands.DirectorySizeCommand")!;
            ICommand directorySizeInstance = (ICommand)Activator.CreateInstance(typeDirectorySize, args[0])!;

            directorySizeInstance.Execute();

            Type typeSearchFiles = assembly.GetType("FileSystemCommands.FindFilesCommand")!;
            ICommand searchInstance = (ICommand)Activator.CreateInstance(typeSearchFiles, Directory.GetCurrentDirectory(), args[1])!;

            searchInstance.Execute();

        }
    }
}
