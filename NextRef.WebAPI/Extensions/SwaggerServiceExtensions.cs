using Microsoft.OpenApi.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.WebAPI.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.MapType<UserId>(() => new OpenApiSchema { Type = "string", Format = "uuid" });
            c.MapType<UserCollectionId>(() => new OpenApiSchema { Type = "string", Format = "uuid" });
            c.MapType<UserCollectionItemId>(() => new OpenApiSchema { Type = "string", Format = "uuid" });
            c.MapType<ContentId>(() => new OpenApiSchema { Type = "string", Format = "uuid" });
            c.MapType<ContributionId>(() => new OpenApiSchema { Type = "string", Format = "uuid" });
            c.MapType<ContributorId>(() => new OpenApiSchema { Type = "string", Format = "uuid" });
            c.MapType<ContentMentionId>(() => new OpenApiSchema { Type = "string", Format = "uuid" });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Annote API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Exemple: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}