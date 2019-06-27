using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xo.Jirabot.Engine;
using xo.Jirabot.WinService.Contracts;
using xo.Jirabot.WinService.PeriodicTasks;

namespace xo.Jirabot.WinService.Service
{
    public class ServiceRunner : IServiceRunner
    {
        private IList<PeriodicTask> __runners;

        private EngineContext __context = EngineContext.Instance();

        public ServiceRunner()
        {
        }

        public void Continued()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            Parallel.ForEach(GetRunners(), run => run.Cancel());
        }

        public void ShutDown()
        {
            Parallel.ForEach(GetRunners(), run => run.Cancel());
        }

        public void Start()
        {
            Parallel.ForEach(GetRunners(), async run => await HandleRun(run));
        }

        public void Stop()
        {
            Parallel.ForEach(GetRunners(), run => run.Cancel());
        }

        private async Task HandleRun(PeriodicTask task)
        {
            try
            {
                await task.Run();
            }
            catch (Exception ex)
            {
                __context.Logger.WriteError(ex.Message);
            }
        }

        private IList<PeriodicTask> GetRunners()
        {
            return __runners ?? (__runners = new List<PeriodicTask>
            {
                new PeriodicTask
                {
                    Action = ObserveJira,
                    Period = TimeSpan.FromMinutes(1),
                    CancelationCallback = () => OnServiceJiraCancelation()
                },
                new PeriodicTask
                {
                    Action = ObserveMattermost,
                    Period = TimeSpan.FromMinutes(2),
                    CancelationCallback = () => OnServiceMattermostCancelation()
                }
            });
        }

        private void ObserveJira()
        {

        }

        private void ObserveMattermost()
        {

        }

        private void OnServiceJiraCancelation()
        {
            __context.Logger.WriteInfo("Jira observer stopped. Service is not running.");
        }

        private void OnServiceMattermostCancelation()
        {
            __context.Logger.WriteInfo("Mattermost observer stopped. Service is not running.");
        }
    }
}
