using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Observer
{
    public interface IServiceSubject
    {
        void Subscribe(IServiceObserver observer);

        void UnSubscribe(IServiceObserver observer);

        void Notify();
    }
}
