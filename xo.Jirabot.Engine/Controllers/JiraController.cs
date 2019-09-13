using System;
using xo.Jirabot.Contracts.Entities.Tasks;
using xo.Jirabot.Contracts.Controllers;
using xo.Jirabot.Engine.Helpers;
using xo.Jirabot.Contracts.Globals;
using xo.Jirabot.Engine.Models;
using xo.Jirabot.Engine.Requests;

namespace xo.Jirabot.Engine.Controllers
{
    public class JiraController : BaseController, IJiraController
    {
        public void CreateJiraTasks()
        {
            var queries = JiraRepository.GetAllQueries();

            if (queries != null)
            {
                foreach (var query in queries)
                {
                    if (TaskRepository.IsPlanned(query.Id))
                    {
                        continue;
                    }

                    var latestRun = TaskRepository.GetLatestRunByReference(query.Id)?.ProcessedTime.Value ?? DateTime.Now;

                    if (FrequencyHelper.IsTimeToPlan(query.Frequency, latestRun, out DateTime plannedRunTime))
                    {
                        TaskRepository.CreateTask(new Task
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

        public void SearchJira()
        {
            var jiraTask = TaskRepository.GetJiraPlanned();

            if (jiraTask == null)
            {
                Logger.WriteInfo($"No JIRA tasks planned for the run at: { DateTime.Now.ToString(Constants.DateTimeFormat) }");

                return;
            }

            var jiraQuery = JiraRepository.GetQueryById(jiraTask.Reference);

            if (jiraQuery == null)
            {
                Logger.WriteError($"No JIRA query found with id: { jiraTask.Reference }");

                return;
            }

            var success = new Action<string>((response) =>
            {
                TaskRepository.CreateTask(new Task
                {
                    PlannedTime = DateTime.Now.AddMinutes(5),
                    Type = TaskType.MATTERMOST,
                    Status = TaskStatus.PLANNED,
                    Reference = jiraQuery.Id
                });

                TaskRepository.UpdateStatus(jiraQuery.Id, TaskStatus.COMPLETED);
            });

            var fail = new Action<string>((message) =>
            {
                TaskRepository.UpdateStatus(jiraQuery.Id, TaskStatus.FAILED);

                Logger.WriteError(message);
            });

            var request = new JiraRequestManager();

            request.Search(new JiraConfiguration
            {
                BaseAddress = "https://jira.devfactory.com",
                Username = "mbigun",
                Password = "Pifmgr2007!",
                ApiPath = "/rest/api/2/search",
                Query = "fields=key&jql=assignee=mbigun&maxResults=10"
            },
            success, fail).ConfigureAwait(false);
        }
    }
}
