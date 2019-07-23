using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Reflection;

namespace OneAspNet.Repository.Dapper
{
    public static class DbProviderFactories
    {
        private static DbProviderFactory _sqlFactory = null;
        private static DbProviderFactory _mysqlFactory = null;
        private static DbProviderFactory _sqliteFactory = null;
        private static ConcurrentDictionary<string, DbProviderFactory> _factories = new ConcurrentDictionary<string, DbProviderFactory>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">        
        /// 数据库工厂类型, 需要引用对应的数据库client库, 
        /// 已知工厂类型 MySql,SqlServer,Sqlite,
        /// 未知类型使用全限定类型名，如（"MySql.Data.MySqlClient.MySqlClientFactory, MySqlConnector"）
        /// </param>        
        /// <returns></returns>
        public static DbProviderFactory GetFactory(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentException("is null or empty", providerName);
            }

            if (string.Equals(providerName, "MySql", StringComparison.OrdinalIgnoreCase))
            {
                if (_mysqlFactory == null)
                {
                    _mysqlFactory = CreateFactory("MySql.Data.MySqlClient.MySqlClientFactory, MySqlConnector");
                }
                return _mysqlFactory;
            }

            if (string.Equals(providerName, "SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                if (_sqlFactory == null)
                {
                    _sqlFactory = CreateFactory("System.Data.SqlClient.SqlClientFactory, System.Data.SqlClient");
                }
                return _sqlFactory;
            }

            if (string.Equals(providerName, "Sqlite", StringComparison.OrdinalIgnoreCase))
            {
                if (_sqliteFactory == null)
                {
                    _sqliteFactory = CreateFactory("Microsoft.Data.Sqlite.SqliteFactory, Microsoft.Data.Sqlite");
                }
                return _sqliteFactory;
            }

            return _factories.GetOrAdd(providerName, CreateFactory);
        }

        private static DbProviderFactory CreateFactory(string typeName)
        {
            DbProviderFactory val = null;
            var type = Type.GetType(typeName);
            if (type != null)
            {
                var propertyInfo = type.GetTypeInfo().GetProperty("Instance");
                if (propertyInfo != null)
                {
                    val = propertyInfo.GetValue(type) as DbProviderFactory;
                }

                if (val == null)
                {
                    var fieldInfo = type.GetTypeInfo().GetField("Instance");
                    val = fieldInfo.GetValue(type) as DbProviderFactory;
                }

                if (val == null)
                {
                    val = Activator.CreateInstance(type) as DbProviderFactory;
                }
            }

            if (val == null)
            {
                throw new TypeLoadException(typeName);
            }

            return val;
        }
    }
}
