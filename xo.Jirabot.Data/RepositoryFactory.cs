using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using xo.Jirabot.Contracts;

namespace xo.Jirabot.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private ITargetDatabase __database = null;

        private IDictionary<string, object> __cache = new Dictionary<string, object>();

        public RepositoryFactory(ITargetDatabase database)
        {
            __database = database;
        }

        public T Get<T>() where T: IRepository
        {
            try
            {
                var type = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeof(T).IsAssignableFrom(p) && !p.IsAbstract).FirstOrDefault();

                if (type == null)
                {
                    throw new Exception("Could not resolve repository type.");
                }
                else
                {
                    if (!__cache.ContainsKey(type.Name))
                    {
                        __cache.Add(type.Name, Activator.CreateInstance(type, __database));
                    }
                }

                return (T)__cache[type.Name];
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.ToString());

                throw exception;
            }
        }
    }
}
