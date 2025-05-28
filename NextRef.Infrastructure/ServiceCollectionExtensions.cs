using Microsoft.Extensions.Configuration;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NextRef.Domain.Contents;

namespace NextRef.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSingleton(new DapperContext(connectionString));

        services.AddScoped<IContentRepository, ContentRepository>();

        return services;
    }
}