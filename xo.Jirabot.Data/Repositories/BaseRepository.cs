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

        protected int Post(string query)
        {
            return Post(query, null);
        }

        protected int Post(string query, IDictionary<string, object> parameters)
        {
            return __database.ExecuteNonQuery(query, parameters);
        }

        protected abstract T MapEntity(IDataRecord record);

        protected T ValueOrDefault<T>(IDataRecord record, string name)
        {
            if (record == null)
            {
                return default(T);
            }

            var index = record.GetOrdinal(name);

            if (index < 0)
            {
                return default(T);
            }

            if (record.IsDBNull(index))
            {
                return default(T);
            }

            return (T)record[name];
        }
    }
}
