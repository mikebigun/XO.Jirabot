using xo.Jirabot.Contracts;

namespace xo.Jirabot.Contracts
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates the instance of repository by entity type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> CreateRepository<T>() where T : IEntity;
    }
}
