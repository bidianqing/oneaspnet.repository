using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Data.Common;

namespace OneAspNet.Repository.Dapper
{
    public class ConnectionFactory
    {
        private readonly DapperOptions _options;

        public ConnectionFactory(IOptionsMonitor<DapperOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.CurrentValue;
        }

        public DbConnection CreateConnection()
        {
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(_options.DatabaseType.ToString());
            DbConnection dbConnection = dbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = _options.ConnectionString;

            return dbConnection;
        }

    }
}
