using Microsoft.Extensions.DependencyInjection;
using NextRef.Application.Contents.Commands.CreateContent;
using NextRef.Application.Users.Services;

namespace NextRef.Application;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<CreateContentCommand>());

        services.AddScoped<ISignInService, SignInService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
