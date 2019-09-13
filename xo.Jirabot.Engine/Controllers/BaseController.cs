using xo.Jirabot.Contracts.Logging;
using xo.Jirabot.Contracts.Repositories;

namespace xo.Jirabot.Engine.Controllers
{
    public abstract class BaseController
    {
        protected ILogger Logger => EngineContext.Instance().Logger;
        protected ITaskReporsitory TaskRepository => EngineContext.Instance().Factory.Get<ITaskReporsitory>();
        protected IJiraRequestRepository JiraRepository => EngineContext.Instance().Factory.Get<IJiraRequestRepository>();
    }
}
