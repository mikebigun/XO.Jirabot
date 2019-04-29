using System;
using System.Collections.Generic;
using System.Data.Common;

namespace xo.Jirabot.Contracts
{
    public interface ITargetDatabase
    {
        DbDataReader ExecuteReader(string query, IDictionary<string, object> parameters = null);

        int ExecuteNonQuery(string query, IDictionary<string, object> parameters = null);

        object ExecuteScalar(string query, IDictionary<string, object> parameters = null);
    }
}
