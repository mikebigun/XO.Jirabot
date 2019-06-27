﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace xo.Jirabot.WinService.PeriodicTasks
{
    public class PeriodicTask : IDisposable
    {
        private CancellationTokenSource __source = new CancellationTokenSource();

        public TimeSpan Period { get; set; }

        public Action Action { get; set; }        

        public Action CancelationCallback { private get; set; }

        public PeriodicTask()
        {
        }

        public async Task Run()
        {
            if (Action == null)
            {
                throw new ArgumentNullException(nameof(Action));
            }

            if (Period == null)
            {
                throw new ArgumentNullException(nameof(Period));
            }

            if (CancelationCallback != null)
            {
                __source.Token.Register(() => CancelationCallback());
            }

            __source.Token.ThrowIfCancellationRequested();

            await Run(__source.Token);
        }

        private async Task Run(CancellationToken cancelationToken)
        {
            while (!cancelationToken.IsCancellationRequested)
            {
                await Task.Delay(Period, cancelationToken);

                if (!cancelationToken.IsCancellationRequested)
                    Action();
            }
        }

        public void Cancel()
        {
            __source.Cancel();
        }

        public void Dispose()
        {            
            __source.Dispose();
        }
    }
}
