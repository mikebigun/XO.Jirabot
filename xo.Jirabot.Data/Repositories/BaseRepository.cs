using System.Collections.Generic;
using System.Data;
using xo.Jirabot.Contracts;

namespace xo.Jirabot.Data.Repositories
{
    public abstract class BaseRepository<T> where T: IEntity
    {
        private ITargetDatabase __database;

        public BaseRepository(ITargetDatabase database)
        {
            __database = database;
        }

        protected IEnumerable<T> Get(string query)
        {
            return Get(query, null);
        }

        protected IEnumerable<T> Get(string query, IDictionary<string, object> parameters)
        {
            return __database.ExecuteReader(query, parameters, MapEntity);
        }

        protected int Update(string query)
        {
            return Update(query, null);
        }

        protected int Update(string query, IDictionary<string, object> parameters)
        {
            return __database.ExecuteNonQuery(query, parameters);
        }

        protected abstract T MapEntity(IDataRecord record);
    }
}
