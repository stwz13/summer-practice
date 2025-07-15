public interface ICommand
{
    void Execute();
}
public interface IExceptionHandler
{
    void HandleException(ICommand command, Exception ex);

}
