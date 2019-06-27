using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities;
using xo.Jirabot.Contracts.Entities.Tasks;
using xo.Jirabot.Contracts.Repositories;

namespace xo.Jirabot.Engine
{
    public class EngineExecutor
    {
        private ITaskReporsitory __taskRepositor;

        private IJiraRequestRepository _jiraRepository;

        private EngineContext __context = EngineContext.Instance();

        public EngineExecutor()
        {
            __taskRepositor = __context.Factory.Get<ITaskReporsitory>();

            _jiraRepository = __context.Factory.Get<IJiraRequestRepository>();
        }


        public void RunJiraObserver()
        {
            var repo = __context.Factory.Get<ITaskReporsitory>();

            // run jira api form tasks
            // create mattermost task when api executed
            
            
        }

        public void RunMattermostObserver()
        {
            // run mattermost api
        }

        public void RunDemandObserver()
        {
            var queries = _jiraRepository.GetAllQueries();

            if (queries != null)
            {
                foreach (var query in queries)
                {
                    __taskRepositor.CreateTask(new Task
                    {
                        Type = TaskType.JIRA,
                        Status = TaskStatus.PLANNED,
                        Request = query.Id
                    });
                }
            }

            // create jira task for every query, (every 15 mins)
        }
    }
}
