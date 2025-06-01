using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NextRef.Application.Behaviors;
using NextRef.Application.Contents.Commands.CreateContent;
using NextRef.Application.Contents.Services;

namespace NextRef.Application;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateContentCommandValidator>();

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
