using FileSystemCommands;
using CommandRun;

namespace task08tests
{
    public class FileSystemCommandsTests
    {
        [Fact]
        public void DirectorySizeCommand_ShouldCalculateSize()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
            File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

            var infOfTestDir = new DirectoryInfo(testDir);
            long sizeOfTestDir = infOfTestDir.GetFiles().Select(f => f.Length).Sum();

            var command = new DirectorySizeCommand(testDir);
            command.Execute();
            
            Assert.Equal(sizeOfTestDir, command.SizeOfDirectory);
            Directory.Delete(testDir, true);
        }

        [Fact]
        public void FindFilesCommand_ShouldFindMatchingFiles()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
            File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

            var command = new FindFilesCommand(testDir, "*.txt");
            command.Execute();

            Assert.Single(command.FilesWithMask);
            Assert.Contains("file1.txt", command.FilesWithMask.Select(f => f.Name));
            Directory.Delete(testDir, true);
        }
        [Fact]
        public void ConsoleApp_WorksCorrect()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
            File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

            var args = new string[] { testDir, "*.txt" };
            CommandRunner.Main(args);

            var infOfTestDir = new DirectoryInfo(testDir);
            long sizeOfTestDir = infOfTestDir.GetFiles().Select(f => f.Length).Sum();

            Assert.Contains("Размер директории: " + sizeOfTestDir, output.ToString());
            Assert.Contains("test1", output.ToString());
            Assert.Contains("test2", output.ToString());
        }
    }
}
