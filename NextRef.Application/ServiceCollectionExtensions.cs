using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NextRef.Application.Behaviors;
using NextRef.Application.Features.Contents.Commands.CreateContent;
using NextRef.Application.Features.Contents.Services;

namespace NextRef.Application;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateContentCommandValidator>();

        // Register MediatR and behaviors
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateContentCommand>();
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddScoped<IContributionService, ContributionService>();

        return services;
    }
}
