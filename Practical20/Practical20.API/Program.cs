var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddApiVersioningServices();

builder.Services.AddSwaggerService();

builder.Services.AddControllers();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithVersioning();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
