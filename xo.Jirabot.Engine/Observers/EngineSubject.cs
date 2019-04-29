using System.Collections.Generic;
using xo.Jirabot.Contracts.Observer;

namespace xo.Jirabot.Engine.Observers
{
    public class EngineSubject : IServiceSubject
    {
        private IList<IServiceObserver> __observers = null;

        public EngineSubject()
        {
            __observers = new List<IServiceObserver>();
        }

        public void Notify()
        {
            foreach (var observer in __observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IServiceObserver observer)
        {
            if (__observers.Contains(observer))
            {
                return;
            }
            __observers.Add(observer);
        }

        public void UnSubscribe(IServiceObserver observer)
        {
            if (__observers.Contains(observer))
            {
                __observers.Remove(observer);
            }
        }
    }
}
