using System;
using System.Collections.Generic;
using System.Data;

namespace xo.Jirabot.Contracts
{
    public interface ITargetDatabase
    {
        IEnumerable<T> ExecuteReader<T>(string query, IDictionary<string, object> parameters, Func<IDataRecord, T> mapper);

        int ExecuteNonQuery(string query, IDictionary<string, object> parameters);
    }
}
