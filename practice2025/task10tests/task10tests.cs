using Plugins;
using task10;

namespace task10tests
{
    public class SystemOfPlaginsTests
    {

        [Fact]
        public void ExecuteTheSystemOfPlugins_ExecutesAllCommands()
        {
            string currDirectory = Directory.GetCurrentDirectory();
            string solutionDirectory = Path.GetFullPath(Path.Combine(currDirectory, "..", "..", "..", ".."));

            var outPut = new StringWriter();
            Console.SetOut(outPut);


            var newSystem = new SystemOfPlugins(solutionDirectory);

            newSystem.ExecuteAllCommands();


            Assert.Contains("Plugin1 command executed", outPut.ToString());
            Assert.Contains("Plugin2 command executed", outPut.ToString());
            Assert.Contains("Plugin3 command executed", outPut.ToString());

        }

        [Fact]
        public void ExecuteTheSystemOfPlugins_SortPluginsInCorrectOrder()
        {
            string currDirectory = Directory.GetCurrentDirectory();
            string solutionDirectory = Path.GetFullPath(Path.Combine(currDirectory, "..", "..", "..", ".."));

            var newSystem = new SystemOfPlugins(solutionDirectory);

            var listOfPlagins = newSystem.GetSortedGrafOfPlagins();

            Assert.Equal(typeof(Plugin1), listOfPlagins[0]);
            Assert.Equal(typeof(Plugin2), listOfPlagins[1]);
            Assert.Equal(typeof(Plugin3), listOfPlagins[2]);

        }


    }
}
