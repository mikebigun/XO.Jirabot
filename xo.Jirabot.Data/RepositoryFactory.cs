using System;
using System.Linq;
using xo.Jirabot.Contracts;

namespace xo.Jirabot.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private ITargetDatabase __database = null;

        public RepositoryFactory(ITargetDatabase database)
        {
            __database = database;
        }

        public IRepository<T> CreateRepository<T>() where T: IEntity
        {
            try
            {
                var type = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeof(IRepository<T>).IsAssignableFrom(p)).FirstOrDefault();

                if (type == null)
                {
                    throw new Exception("Could not resolve repository type.");
                }

                return (IRepository<T>)Activator.CreateInstance(type, __database);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
