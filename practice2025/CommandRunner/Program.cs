using System.Collections.Generic;
using System.Reflection;
using CommandLib;
namespace CommandRun
{
    public class CommandRunner
    {
        public static void Main(string[] args)
        {
            string currDirectory = Directory.GetCurrentDirectory();
            string solutionDirectory = Path.GetFullPath(Path.Combine(currDirectory, "..", "..", "..", ".."));
            string searchDllFile = Directory.GetFiles(solutionDirectory, "FileSystemCommands.dll", SearchOption.AllDirectories)[0];

            Assembly assembly = Assembly.LoadFrom(searchDllFile);

            Type typeDirectorySize = assembly.GetType("FileSystemCommands.DirectorySizeCommand")!;
            ICommand directorySizeInstance = (ICommand)Activator.CreateInstance(typeDirectorySize, args[0])!;

            directorySizeInstance.Execute();
            long directorySize = (long)typeDirectorySize.GetProperty("SizeOfDirectory")!.GetValue(directorySizeInstance)!;
            Console.WriteLine($"Размер директории: {directorySize}");

            Type typeSearchFiles = assembly.GetType("FileSystemCommands.FindFilesCommand")!;
            ICommand searchInstance = (ICommand)Activator.CreateInstance(typeSearchFiles, args[0], args[1])!;

            searchInstance.Execute();

            List<FileInfo> searchFiles = (List<FileInfo>)typeSearchFiles.GetProperty("FilesWithMask")!.GetValue(searchInstance)!;
            Console.WriteLine(searchFiles.Count !=0 ? "Найденные файлы:" : "Файлов с заданной маской нет");
            Console.WriteLine(string.Join("\n", searchFiles.Select(file => file.Name)));

        }
    }
}
