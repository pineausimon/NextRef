namespace NextRef.WebAPI.Extensions;

public static class CorsExtensions
{
    private const string CorsPolicyName = "NextRefAppOrigin";

    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: CorsPolicyName,
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        return services;
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
        return app;
    }
}