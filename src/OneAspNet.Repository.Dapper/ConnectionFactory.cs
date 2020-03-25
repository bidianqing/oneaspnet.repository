using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace OneAspNet.Repository.Dapper
{
    public class ConnectionFactory
    {
        private readonly DapperOptions _options;
        private static Dictionary<string, string> _drives = new Dictionary<string, string>()
        {
            { DatabaseType.MySql.ToString(),"MySql.Data.MySqlClient.MySqlClientFactory, MySqlConnector"},
            { DatabaseType.SqlServer.ToString(),"Microsoft.Data.SqlClient.SqlClientFactory, Microsoft.Data.SqlClient"},
            { DatabaseType.Sqlite.ToString(),"Microsoft.Data.Sqlite.SqliteFactory, Microsoft.Data.Sqlite"},
            { DatabaseType.PostgreSql.ToString(),"Npgsql.NpgsqlFactory, Npgsql"}
        };

        public ConnectionFactory(IOptionsMonitor<DapperOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.CurrentValue;

#if NETSTANDARD2_1
            foreach (var item in _drives)
            {
                System.Data.Common.DbProviderFactories.RegisterFactory(item.Key, item.Value);
            }
#endif
        }

        public DbConnection CreateConnection()
        {
            DbProviderFactory dbProviderFactory = null;
#if NETSTANDARD2_1
            dbProviderFactory = System.Data.Common.DbProviderFactories.GetFactory(_options.DatabaseType.ToString());
#else
            dbProviderFactory = DbProviderFactories.GetFactory(_options.DatabaseType.ToString());
#endif
            DbConnection connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = _options.ConnectionString;

            return connection;
        }

    }
}
