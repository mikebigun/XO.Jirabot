using System.Collections.Generic;

namespace xo.Jirabot.Contracts
{
    public interface IRepository<T> where T: IEntity
    {
        T GetById(int id);

        IEnumerable<T> GetMany(string query);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
