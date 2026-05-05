namespace Practical17.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Practical 17 API",
                Version = "v1",
                Description = "Practical 17 API"
            });

            const string schemeId = "bearer";

            options.AddSecurityDefinition(schemeId, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter JWT Bearer token only",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = schemeId,
                BearerFormat = "JWT"
            });

            options.OperationFilter<AuthOperationFilter>();
        });

        return services;
    }

    public static WebApplication UseSwaggerWithVersioning(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Practical 17 API V1");
        });

        return app;
    }
}