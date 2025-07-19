using ServerThreadSystem;
namespace task17tests
{
    public class ServerThreadSystemTests
    {
        public class TestCommand : IServerThreadCommand
        {
            public ServerThread ServerThread { get; }
            public string Message { get; set; } = string.Empty;

            public TestCommand(ServerThread serverThread, string message)
            {
                ServerThread = serverThread;
                Message = message;
            }

            public void Execute() => Console.WriteLine(Message);

        }
        [Fact]
        public void Server_WorkFieldsIsTrue()
        {
            var serverThread = new ServerThread();
            serverThread.Start();

            Assert.True(serverThread.IsWorking);
        }
        [Fact]
        public void Server_SoftEndFieldIsFalse()
        {
            var serverThread = new ServerThread();
            serverThread.Start();

            Assert.False(serverThread.SoftStop);
        }

        [Fact]
        public void Server_EndsWorkWithHardStop()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var serverThread = new ServerThread();
            serverThread.Start();

            serverThread.AddCommand(new HardStopCommand(serverThread));
            serverThread.AddCommand(new TestCommand(serverThread, "TestCommand"));

            Assert.Contains("Поток не запущен", output.ToString());
        }
        [Fact]
        public void Server_ReturnsExceptionWithWrongCommand()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var firstServerThread = new ServerThread();
            var secondServerThread = new ServerThread();
            firstServerThread.Start();

            firstServerThread.AddCommand(new HardStopCommand(secondServerThread));
;
            Assert.Contains("Команда не может быть вызвана для текущего потока", output.ToString());   
        }

        [Fact]
        public void Server_DoesntWorkAfterHardStop()
        {
            var serverThread = new ServerThread();
            serverThread.Start();

            serverThread.AddCommand(new HardStopCommand(serverThread));
            Thread.Sleep(10);
            Assert.False(serverThread.IsWorking);
        }
        [Fact]
        public void Server_CompletesAllCommandWithSoftStop()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var serverThread = new ServerThread();
            serverThread.Start();

            serverThread.AddCommand(new TestCommand(serverThread, "command 1"));
            serverThread.AddCommand(new TestCommand(serverThread, "command 2"));
            serverThread.AddCommand(new TestCommand(serverThread, "command 3"));


            Thread.Sleep(10);
            

            Assert.Contains("command 1", output.ToString());
            Assert.Contains("command 2", output.ToString());
            Assert.Contains("command 3", output.ToString());

        }
        [Fact]
        public void Server_CompletesOnlyItsOwnCommands()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var firstServerThread = new ServerThread();
            var secondServerThread = new ServerThread();

            firstServerThread.Start();

            firstServerThread.AddCommand(new TestCommand(firstServerThread, "command 1"));
            firstServerThread.AddCommand(new TestCommand(secondServerThread, "command 2"));
            firstServerThread.AddCommand(new TestCommand(firstServerThread, "command 3"));

            firstServerThread.AddCommand(new SoftStopCommand(firstServerThread));

            Thread.Sleep(10);


            Assert.Contains("command 1", output.ToString());
            Assert.Contains("Команда не может быть вызвана для текущего потока", output.ToString());
            Assert.Contains("command 3", output.ToString());

        }
    }
}
