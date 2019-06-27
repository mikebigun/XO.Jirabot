using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xo.Jirabot.Engine;
using xo.Jirabot.WinService.Contracts;
using xo.Jirabot.WinService.PeriodicJobs;

namespace xo.Jirabot.WinService.Service
{
    public class ServiceRunner : IServiceRunner
    {
        private IList<PeriodicJob> __jobs;

        private EngineContext __context = EngineContext.Instance();

        private EngineExecutor __engine = new EngineExecutor();

        public ServiceRunner()
        {
        }

        public void Start()
        {
            StartInternal();
        }

        public void Continued()
        {
            StartInternal();
        }

        public void Stop()
        {
            StopInternal();
        }

        public void Pause()
        {
            StopInternal();
        }

        public void ShutDown()
        {
            StopInternal();
        }

        private void StartInternal()
        {
            var loop = Parallel.ForEach(GetJobs(), async run =>
            {
                try
                {
                    await run.Run();
                }
                catch (Exception ex)
                {
                    __context.Logger.WriteError(ex.ToString());
                }
            });

            if (loop.IsCompleted)
            {
                __context.Logger.WriteInfo("Service started successfully.");
            }
        }

        private void StopInternal()
        {
            var loop = Parallel.ForEach(GetJobs(), job => job.Cancel());

            if (loop.IsCompleted)
            {
                __context.Logger.WriteInfo("Service stopped successfully.");
            }
        }

        private IList<PeriodicJob> GetJobs()
        {
            return __jobs ?? (__jobs = new List<PeriodicJob>
            {
                new PeriodicJob
                {
                    Name = "Sheduled Tasks Creator",
                    Action = ObserveSchedules,
                    Period = TimeSpan.FromMinutes(30),
                    CancelationCallback = () => OnServiceDemandCanceled()
                },
                new PeriodicJob
                {
                    Name = "Jira Task Executor",
                    Action = ObserveJira,
                    Period = TimeSpan.FromSeconds(30),
                    CancelationCallback = () => OnServiceJiraCanceled()
                },
                new PeriodicJob
                {
                    Name = "Mattermost Task Executor",
                    Action = ObserveMattermost,
                    Period = TimeSpan.FromSeconds(120),
                    CancelationCallback = () => OnServiceMattermostCanceled()
                }
            });
        }

        private void ObserveSchedules()
        {
            __engine.RunSchedulesObserver();
        }

        private void ObserveJira()
        {
            __engine.RunJiraObserver();
        }

        private void ObserveMattermost()
        {
            __engine.RunMattermostObserver();
        }

        private void OnServiceDemandCanceled()
        {
            __context.Logger.WriteInfo("Demand observer stopped. Service is not running.");
        }

        private void OnServiceJiraCanceled()
        {
            __context.Logger.WriteInfo("Jira observer stopped. Service is not running.");
        }

        private void OnServiceMattermostCanceled()
        {
            __context.Logger.WriteInfo("Mattermost observer stopped. Service is not running.");
        }
    }
}
