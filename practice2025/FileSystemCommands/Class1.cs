using CommandLib;

namespace FileSystemCommands
{
    public class DirectorySizeCommand : ICommand
    {
        public readonly string NameOfDirectory;

        public long SizeOfDirectory { get; private set; }

        public DirectorySizeCommand(string nameOfDirectory) => NameOfDirectory = nameOfDirectory;

        public static long CalculateSizeOfDirectory(string nameOfDirectory)
        {
            var currDirectory = new DirectoryInfo(nameOfDirectory);

            return currDirectory.GetFiles().Select(f => f.Length).Sum();
        }

        public void Execute() => SizeOfDirectory = CalculateSizeOfDirectory(NameOfDirectory);
    
    }

    public class FindFilesCommand : ICommand
    {
        public readonly string NameOfDirectory;
        public readonly string Mask;
        public List<FileInfo> FilesWithMask { get; private set; } = new List<FileInfo>();

        public FindFilesCommand(string nameOfDirectory, string mask)
        {
            NameOfDirectory = nameOfDirectory;
            Mask = mask;
        }

        public static List<FileInfo> SearchFilesWithMask(string nameOfDirectory, string mask)
        {
            var currDirectory = new DirectoryInfo(nameOfDirectory);

            return currDirectory.GetFiles(mask).ToList();
        }

        public void Execute() => FilesWithMask = SearchFilesWithMask(NameOfDirectory, Mask);

    }
}

