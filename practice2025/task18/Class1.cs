using System.Collections.Concurrent;

namespace Scheduler
{
    public class RoundRobinScheduler : IScheduler
    {
        public ConcurrentQueue<ILongCommand> CommandQueue { get; private set; } = new ConcurrentQueue<ILongCommand>();
        private ILongCommand? currCommand;

        public bool HasCommand() => CommandQueue.Count != 0 || currCommand != null;

        public ICommand Select()
        {
            if (currCommand != null && !currCommand.isCompleted) return currCommand;
            if (CommandQueue.TryDequeue(out var nextCommand)) 
            {
                currCommand = nextCommand;
                return currCommand;
            }
            
            currCommand = null;
            return null;
        }
        public void Add(ICommand command)
        {
            if (command is ILongCommand longCommand)
            {
                if (CommandQueue.Contains(longCommand) && longCommand != currCommand) throw new Exception("Планировщик обрабатывает данную команду");

                if (longCommand.isCompleted) return;

                CommandQueue.Enqueue(longCommand);
            }
        }

    }
}