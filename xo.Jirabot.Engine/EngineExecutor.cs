using System;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities;
using xo.Jirabot.Contracts.Entities.Tasks;
using xo.Jirabot.Contracts.Repositories;
using xo.Jirabot.Engine.Helpers;

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

        public void RunSchedulesObserver()
        {
            var queries = _jiraRepository.GetAllQueries();

            if (queries != null)
            {
                foreach (var query in queries)
                {
                    var task = __taskRepositor.GetLatestTaskByReference(query.Id);

                    if (task == null)
                    {
                        continue;
                    }

                    if (FrequencyHelper.MinutesDiffToNow(Convert.ToInt64(task.RunTicks)) >= 
                        Convert.ToInt32(query.Frequency))
                    {
                        __taskRepositor.CreateTask(new Task
                        {
                            Type = TaskType.JIRA,
                            Status = TaskStatus.PLANNED,
                            RunTicks = DateTime.Now.Ticks.ToString(),
                            Reference = query.Id
                        });
                    }
                }
            }

            // create jira task for every query, (every 1 min)
        }
    }
}
