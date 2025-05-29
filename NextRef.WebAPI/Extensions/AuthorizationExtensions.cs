using NextRef.Domain.Core;

namespace NextRef.WebAPI.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserOrAdmin", policy =>
                policy.RequireRole(UserRoles.User, UserRoles.Admin));

            // Tu peux ajouter d'autres policies ici si besoin
        });

        return services;
    }
}