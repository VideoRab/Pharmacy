namespace Identity.Web.IdentityServerSettings;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        if (!context.Clients.Any())
        {
            foreach (var client in Configuration.GetClients())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Configuration.GetIdentityResources())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in Configuration.GetApiScopes())
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
        if (!context.ApiResources.Any())
        {
            foreach (var resource in Configuration.GetApiResources())
            {
                context.ApiResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
    }
}
