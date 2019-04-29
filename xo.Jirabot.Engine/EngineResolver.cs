using Ninject;
using Ninject.Parameters;
using System.Reflection;

namespace xo.Jirabot.Engine
{
    public class EngineResolver
    {
        private static StandardKernel _kernel = new StandardKernel();

        private static bool _isReady;

        private static object _lock = new object();

        public EngineResolver()
        {
            Load();
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        private void Load()
        {
            lock (_lock)
            {
                if (_isReady)
                {
                    return;
                }
                var assembly = Assembly.GetAssembly(typeof(EngineResolver));
                _kernel.Load(assembly);
                _isReady = true;
            }
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public T GetWithConstructorArgument<T>(string name, object value)
        {
            return _kernel.Get<T>(new ConstructorArgument(name, value, true));
        }
    }
}
