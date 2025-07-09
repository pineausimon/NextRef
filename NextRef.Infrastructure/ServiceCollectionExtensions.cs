using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NextRef.Application.Caching;
using NextRef.Domain.Users;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Repositories;
using NextRef.Infrastructure.Authentication;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.UserCollections.Repositories;
using NextRef.Infrastructure.Caching.Redis;
using StackExchange.Redis;
using NextRef.Application.Features.Users.Services;

namespace NextRef.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        var redisConnection = configuration.GetConnectionString("Redis") ?? "localhost:6379";

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        services.AddSingleton(new DapperContext(connectionString));

        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserCollectionRepository, UserCollectionRepository>();
        services.AddScoped<IUserCollectionItemRepository, UserCollectionItemRepository>();
        services.AddScoped<IContributionRepository, ContributionRepository>();
        services.AddScoped<IContributorRepository, ContributorRepository>();
        services.AddScoped<IContentMentionRepository,ContentMentionRepository>();

        services.AddScoped<IUserAuthService, UserAuthService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddSingleton<ICacheService, RedisCacheService>();

        return services;
    }
}