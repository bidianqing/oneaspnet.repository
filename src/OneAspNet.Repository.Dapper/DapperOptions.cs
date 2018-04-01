namespace OneAspNet.Repository.Dapper
{
    public class DapperOptions
    {
        /// <summary>
        /// connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// database type the default value DatabaseType.MySql
        /// </summary>
        public DatabaseType DatabaseType { get; set; } = DatabaseType.MySql;
    }

    public enum DatabaseType
    {
        SqlServer,
        MySql,
    }

}
