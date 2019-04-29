using Ninject.Modules;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Logging;
using xo.Jirabot.Contracts.Observer;
using xo.Jirabot.Data;
using xo.Jirabot.Engine.Logging;
using xo.Jirabot.Engine.Observers;

namespace xo.Jirabot.Engine
{
    public class EngineResolverModule: NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ITargetDatabase>().To<SQLiteDatabase>();
            Bind<IRepositoryFactory>().To<RepositoryFactory>();
            Bind<ILogger>().To<Logger>();
            Bind<IServiceSubject>().To<EngineSubject>();
            Bind<IServiceObserver>().To<EngineObserver>();
        }
    }
}
