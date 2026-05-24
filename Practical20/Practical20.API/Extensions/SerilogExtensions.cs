namespace Practical20.API.Extensions;

public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilogLogging(
        this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection")!;

        var columnOptions = new ColumnOptions
        {
            AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                {
                    ColumnName = "SourceContext",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 500,
                    AllowNull = true
                },
                new SqlColumn
                {
                    ColumnName = "RequestPath",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 500,
                    AllowNull = true
                },
                new SqlColumn
                {
                    ColumnName = "MachineName",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 200,
                    AllowNull = true
                },
                new SqlColumn
                {
                    ColumnName = "EnvironmentName",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 100,
                    AllowNull = true
                },
                new SqlColumn
                {
                    ColumnName = "ThreadId",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 50,
                    AllowNull = true
                }
            }
        };

        columnOptions.Store.Remove(StandardColumn.Properties);
        columnOptions.Store.Add(StandardColumn.LogEvent);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithThreadId()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                path: Path.Combine(AppContext.BaseDirectory, "Logs", "log-.txt"),
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
                retainedFileCountLimit: 30)
            .WriteTo.MSSqlServer(
                connectionString: connectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "Logs",
                    AutoCreateSqlTable = true
                },
                columnOptions: columnOptions)
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }
}