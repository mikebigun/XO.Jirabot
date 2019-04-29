using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using xo.Jirabot.Contracts;

namespace xo.Jirabot.Data
{
    public class SQLiteDatabase : ITargetDatabase
    {
        private SQLiteConnection __connection = null;

        public SQLiteDatabase()
        {
            __connection = new SQLiteConnection(@"Data Source=C:\xo.Jirabot\xo.Jirabot.Data\dbfile\xo.Jirabot.Db;Version=3;");
        }

        public int ExecuteNonQuery(string query, IDictionary<string, object> parameters = null)
        {
            try
            {
                using (var con = CreateOpenConnection())
                {
                    return CreateCommand(con, query, parameters).ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public DbDataReader ExecuteReader(string query, IDictionary<string, object> parameters = null)
        {
            try
            {
                using (var con = CreateOpenConnection())
                {
                    return CreateCommand(con, query, parameters).ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }

        public object ExecuteScalar(string query, IDictionary<string, object> parameters = null)
        {
            try
            {
                using (var con = CreateOpenConnection())
                {
                    return CreateCommand(con, query, parameters).ExecuteScalar();
                }
            }
            catch
            {
                throw;
            }
        }

        private SQLiteConnection CreateOpenConnection()
        {
            if (__connection.State != ConnectionState.Open)
            {
                __connection.Open();
            }
            return __connection;
        }

        private SQLiteCommand CreateCommand(SQLiteConnection connection, string query, IDictionary<string, object> parameters = null)
        {
            var command = new SQLiteCommand
            {
                Connection = connection,
                CommandType = CommandType.Text,
                CommandText = query
            };

            if (parameters != null && parameters.Count > 0)
            {
                FillParameters(command, parameters);
            }
            return command;
        }

        private void FillParameters(DbCommand command, IDictionary<string, object> parameters)
        {
            if (command.Parameters.Count > 0)
            {
                command.Parameters.Clear();
            }

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(new SQLiteParameter()
                {
                    DbType = MapParameterType(parameter.Value.GetType()),
                    Direction = ParameterDirection.Input,
                    ParameterName = parameter.Key,
                    Value = parameter.Value == null ? DBNull.Value : parameter.Value
                });
            }
        }

        private DbType MapParameterType(Type type)
        {
            switch (type.Name)
            {
                case "Int32":                
                    return DbType.Int32;
                case "Int64":
                    return DbType.Int64;
                case "String":
                    return DbType.String;
                case "Boolean":
                    return DbType.Int32;
                case "Object":
                    return DbType.String;
                case "DateTime":
                    return DbType.String;
                default:
                    throw new Exception("SQLite data type is not supported.");
            }
        }
    }
}
