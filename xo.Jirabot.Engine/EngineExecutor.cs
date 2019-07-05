using System;
using xo.Jirabot.Contracts.Entities.Tasks;
using xo.Jirabot.Contracts.Repositories;
using xo.Jirabot.Engine.Helpers;

namespace xo.Jirabot.Engine
{
    public class EngineExecutor
    {
        private ITaskReporsitory __taskRepository;

        private IJiraRequestRepository _jiraRepository;

        private EngineContext __context = EngineContext.Instance();

        public EngineExecutor()
        {
            __taskRepository = __context.Factory.Get<ITaskReporsitory>();

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
            // runs every min and checks Failed/Completed tasks
            // for every completed task it gets frequency of query
            // and creates new Planned task 

            var queries = _jiraRepository.GetAllQueries();

            if (queries != null)
            {
                foreach (var query in queries)
                {
                    if (__taskRepository.IsPlanned(query.Id))
                    {
                        continue;
                    }
                    
                    var latestRun = __taskRepository.GetLatestRunByReference(query.Id);

                    var latestRunTime = latestRun == null ? 
                        DateTime.Now: 
                        latestRun.ProcessedTime.Value;

                    var plannedRunTime = DateTime.MinValue;

                    if (FrequencyHelper.IsTimeToPlan(query.Frequency, latestRunTime, out plannedRunTime))
                    {
                        __taskRepository.CreateTask(new Task
                        {
                            Type = TaskType.JIRA,
                            Status = TaskStatus.PLANNED,
                            PlannedTime = plannedRunTime,
                            Reference = query.Id
                        });
                    }
                }
            }
        }
    }
}
