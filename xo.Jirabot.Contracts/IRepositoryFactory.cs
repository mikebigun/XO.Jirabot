namespace xo.Jirabot.Contracts
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates the instance of repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>() where T : IRepository;
    }
}
