using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NextRef.Application.Behaviors;
using NextRef.Application.Contents.Commands.CreateContent;
using NextRef.Application.Contents.Services;

namespace NextRef.Application;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<CreateContentCommand>());


        services.AddValidatorsFromAssemblyContaining<CreateContentCommandValidator>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IContributionService, ContributionService>();

        return services;
    }
}
