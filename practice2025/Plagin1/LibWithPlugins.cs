using PluginLib;

namespace Plugins
{
    [PluginLoad(new Type[] { })]
    public class Plugin1 : IPlugin
    {
        public void Execute() => Console.WriteLine("Plugin1 command executed");
    }
    [PluginLoad(new Type[] { typeof(Plugin1) })]
    public class Plugin2 : IPlugin
    {
        public void Execute() => Console.WriteLine("Plugin2 command executed");
    }
    [PluginLoad (new Type[] { typeof(Plugin1), typeof(Plugin2)})]
    public class Plugin3 : IPlugin
    {
        public void Execute() => Console.WriteLine("Plugin3 command executed");
    }

}

