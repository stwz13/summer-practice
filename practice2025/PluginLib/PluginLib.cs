namespace PluginLib
{
    public interface IPlugin
    {
        void Execute();
    }
    public class PluginLoadAttribute : Attribute
    {
        public Type[] PlaginsDepends { get; private set; }

        public PluginLoadAttribute(Type[] plaginsDepends) => PlaginsDepends = plaginsDepends;

    }
}
