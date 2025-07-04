
using ConsoleApp;
namespace task09tests
{
    public class TestClass
    {
        int Field;
        public TestClass(int field) => Field = field;
        public static void TestMethod(int param1) { }
    }
    public class Task10tests
    {

        [Fact]
        public void PrintMethodInfo_PrintsCorrectInformation()
        {

            var output = new StringWriter();
            Console.SetOut(output);

            var testMethodInf = typeof(TestClass).GetMethod("TestMethod")!;
            ConsolePrinter.PrintMethodInfo(testMethodInf);
            Console.WriteLine(output.ToString());
            Assert.Contains("Имя метода: TestMethod", output.ToString());
            Assert.Contains("Имя параметра - param1, тип параметра - System.Int32", output.ToString());
        }
        [Fact]
        public void PrintConstructInfo_PrintsCorrectInformation()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var testConstInf = typeof(TestClass).GetConstructor(new[] {typeof(int)})!;
            ConsolePrinter.PrintConstructorInfo(testConstInf);

            Assert.Contains("Имя конструктора: .ctor", output.ToString());
            Assert.Contains("Имя параметра - field, тип параметра - System.Int32", output.ToString());

        }
        [Fact]
        public void PrintType_PrintsCorrectInformation()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            ConsolePrinter.PrintTypeInfo(typeof(TestClass));
            Assert.Contains($"Имя класса: {typeof(TestClass).FullName}", output.ToString());
            Assert.Contains("Имя метода: TestMethod", output.ToString());
            Assert.Contains("Имя параметра - param1, тип параметра - System.Int32", output.ToString());
            Assert.Contains("Имя конструктора: .ctor", output.ToString());
            Assert.Contains("Имя параметра - field, тип параметра - System.Int32", output.ToString());
        }
        [Fact]
        public void Main_PrintAllClasses()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            string currDirectory = Directory.GetCurrentDirectory();
            string solutionDirectory = Path.GetFullPath(Path.Combine(currDirectory, "..", "..", "..", ".."));

            string dllPath = Directory.GetFiles(solutionDirectory, "task07.dll", SearchOption.AllDirectories)[0];

            var args = new string[] { dllPath };

            ConsolePrinter.Main(args);

            Assert.Contains("Имя класса: task07.DisplayNameAttribute", output.ToString());
            Assert.Contains("Имя класса: task07.VersionAttribute", output.ToString());
        }

    } 
}
