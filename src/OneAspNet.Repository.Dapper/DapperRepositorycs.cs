using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OneAspNet.Repository.Dapper
{
    public class DapperRepository
    {
        private readonly static Dictionary<DatabaseType, Type> dic = new Dictionary<DatabaseType, Type>
        {
            [DatabaseType.SqlServer] = typeof(SqlConnection),
            [DatabaseType.MySql] = typeof(MySqlConnection)
        };

        private readonly DapperOptions _options;
        public IDbConnection Connection;

        public DapperRepository(IOptions<DapperOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;
            Type type = dic[_options.DatabaseType];
            Connection = Activator.CreateInstance(type) as IDbConnection;
            Connection.ConnectionString = _options.ConnectionString;
        }

    }

}
