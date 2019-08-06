using System;
using xo.Jirabot.Contracts.Entities.Tasks;
using xo.Jirabot.Contracts.Globals;
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
            var jiraTask = __taskRepository.GetJiraPlanned();

            if (jiraTask == null)
            {
                __context.Logger.WriteInfo($"No JIRA tasks planned for the run at: { DateTime.Now.ToString(Constants.DateTimeFormat) }");

                return;
            }

            var jiraQuery = _jiraRepository.GetQueryById(jiraTask.Reference);

            if (jiraQuery == null)
            {
                __context.Logger.WriteError($"No JIRA query found with id: { jiraTask.Reference }");

                return;
            }

            var success = new Action(() =>
            {
                __taskRepository.CreateTask(new Task
                {
                    PlannedTime = DateTime.Now.AddMinutes(5),
                    Type = TaskType.MATTERMOST,
                    Status = TaskStatus.PLANNED,
                    Reference = jiraQuery.Id
                });

                __taskRepository.UpdateStatus(jiraQuery.Id, TaskStatus.COMPLETED);
            });

            var fail = new Action(() => 
            {
                __taskRepository.UpdateStatus(jiraQuery.Id, TaskStatus.FAILED);
            });

            // run api call 
            
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
                    
                    var latestRun = __taskRepository.GetLatestRunByReference(query.Id)?.ProcessedTime.Value ?? DateTime.Now;

                    if (FrequencyHelper.IsTimeToPlan(query.Frequency, latestRun, out DateTime plannedRunTime))
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
