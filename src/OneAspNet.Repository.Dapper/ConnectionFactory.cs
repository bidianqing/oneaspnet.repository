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
        };

        public ConnectionFactory(IOptionsMonitor<DapperOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.CurrentValue;

            foreach (var item in _drives)
            {
                DbProviderFactories.RegisterFactory(item.Key, item.Value);
            }
        }

        public DbConnection CreateConnection()
        {
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(_options.DatabaseType.ToString());
            DbConnection connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = _options.ConnectionString;

            return connection;
        }

    }
}
