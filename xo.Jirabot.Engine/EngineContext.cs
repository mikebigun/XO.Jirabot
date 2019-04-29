using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Logging;

namespace xo.Jirabot.Engine
{
    public class EngineContext
    {
        private static EngineContext __instance = null;

        private EngineResolver __resolver = null;

        private ILogger __logger = null;

        private IRepositoryFactory __factory = null;

        private EngineContext()
        {
            __resolver = new EngineResolver();
        }

        public static EngineContext Instance()
        {
            return __instance ?? (__instance = new EngineContext());
        }

        /// <summary>
        /// Gets the database logger.
        /// </summary>
        public ILogger Logger
        {
            get { return __logger ?? (__logger = __resolver.Get<ILogger>()); }
        }

        /// <summary>
        /// Gets the factory of repositories.
        /// </summary>
        public IRepositoryFactory Factory
        {
            get
            {
                return __factory ?? 
                    (__factory = __resolver.GetWithConstructorArgument<IRepositoryFactory>(
                    "database",
                    __resolver.Get<ITargetDatabase>()));
            }
        }
    }
}
