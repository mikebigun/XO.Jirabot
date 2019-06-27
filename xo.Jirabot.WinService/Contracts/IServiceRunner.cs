using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.WinService.Contracts
{
    interface IServiceRunner
    {
        void Start();

        void Stop();

        void Pause();

        void Continued();

        void ShutDown();
    }
}
