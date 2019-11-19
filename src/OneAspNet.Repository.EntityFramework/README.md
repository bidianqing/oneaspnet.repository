此项目不再维护，作者认为可以在项目里直接使用，开箱即用  
```
services.AddDbContextPool<YourDbContext>(options =>  // replace "YourDbContext" with the class name of your DbContext
{
    // Install >> Pomelo.EntityFrameworkCore.MySql
    options.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"));

    //options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnectionString"));
});

```
