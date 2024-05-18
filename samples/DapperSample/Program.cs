var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDapper(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("MySqlConnectionString");
    options.DatabaseType = DatabaseType.MySql;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
