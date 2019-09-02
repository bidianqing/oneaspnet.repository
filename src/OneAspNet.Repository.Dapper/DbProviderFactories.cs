using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace OneAspNet.Repository.Dapper
{
    public static class DbProviderFactories
    {
        private static ConcurrentDictionary<string, DbProviderFactory> _factories = new ConcurrentDictionary<string, DbProviderFactory>(StringComparer.OrdinalIgnoreCase);

        private static Dictionary<string, string> _drives = new Dictionary<string, string>()
        {
            { "MySql","MySql.Data.MySqlClient.MySqlClientFactory, MySqlConnector"},
            { "SqlServer","Microsoft.Data.SqlClient.SqlClientFactory, Microsoft.Data.SqlClient"},
            { "Sqlite","Microsoft.Data.Sqlite.SqliteFactory, Microsoft.Data.Sqlite"},
        };

        public static DbProviderFactory GetFactory(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentException("is null or empty", providerName);
            }

            return _factories.GetOrAdd(providerName, (pName) =>
            {
                return CreateFactory(_drives[pName]);
            });
        }


        private static DbProviderFactory CreateFactory(string typeName)
        {
            var type = Type.GetType(typeName);

            if (type == null)
            {
                throw new ArgumentException($"{typeName} can not load");
            }

            var providerInstance = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static);

            if (providerInstance == null)
            {
                throw new InvalidOperationException(typeName);
            }

            if (!providerInstance.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
            {
                throw new InvalidOperationException(typeName);
            }

            object factory = providerInstance.GetValue(null);
            if (null == factory)
            {
                throw new InvalidOperationException(typeName);
            }

            return (DbProviderFactory)factory;
        }
    }
}
