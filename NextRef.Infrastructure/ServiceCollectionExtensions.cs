using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NextRef.Domain.Contents;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Repositories;
using NextRef.Infrastructure.Authentication;

namespace NextRef.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton(new DapperContext(connectionString));

        services.AddScoped<IContentRepository, ContentRepository>();


        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}