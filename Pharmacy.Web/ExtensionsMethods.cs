using Common.Mappers;
using Common.Services;
using Common.Services.Interfaces;
using DAL.Abstraction;
using DAL.Data;
using DAL.Repositories;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Pharmacy.Web;

public static class ExtensionMethods
{
    public static void ConfigureDbInitializer(this IServiceCollection services)
    {
        services.AddTransient<IDbInitializer, DbInitializer>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IMedicineRepository, MedicineRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IMedicineService, MedicineService>();
        services.AddTransient<IOrderService, OrderService>();
    }

    public static void AddMappers(this IServiceCollection services)
    {
        services.AddTransient<MedicineMapper>();
        services.AddTransient<OrderMapper>();
        services.AddTransient<UserMapper>();
    }
  
    public static void ConfigureAuthService(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        var identityUrl = configuration.GetValue<string>("IdentityUrl");

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Bearer";
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(options =>
        {
            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = "pharmacy";
        });
    }
    public static void DbInitialize(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        dbInitializer.DbInitialize();
    }
    public static void AddSwaggerGenWithAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = "Innogotchi Swagger API",
                Title = "Swagger with Identity Server 4",
                Version = "1.0.0"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri(configuration.GetValue<string>("IdentityTokenUrl")!),
                        Scopes = new Dictionary<string, string>
                        {
                            {"pharmacy", "pharmacy"}
                        }
                    }
                },
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
    }
}
