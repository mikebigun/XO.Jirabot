using System;
using System.Diagnostics;
using Topshelf;
using xo.Jirabot.WinService.Contracts;
using xo.Jirabot.WinService.Service;

namespace xo.Jirabot.WinService
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = HostFactory.Run(host =>
            {
                host.Service<IServiceRunner>(service =>
                {
                    service.ConstructUsing(name => new ServiceRunner());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                    service.WhenPaused(s => s.Pause());
                    service.WhenContinued(s => s.Continued());
                    service.WhenShutdown(s => s.ShutDown());
                });

                host.OnException(ex => OnServiceException(ex));

                host.RunAsPrompt();

                host.SetDescription("XO Jira Bot Win Service");
                host.SetDisplayName("XO Jira Bot");
                host.SetServiceName("XO.Jirabot");
            });

            var exitCode = (int)Convert.ChangeType(factory, factory.GetTypeCode());
            Environment.ExitCode = exitCode;
        }

        private static void OnServiceException(Exception ex)
        {
            Trace.TraceError(ex.Message);
        }
    }
}
