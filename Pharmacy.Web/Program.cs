using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Web.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Pharmacy.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
            builder.Services.ConfigureAuthService(builder.Configuration);

            builder.Services.AddDbContext<ApplicationContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDatabaseConnection"),
                    migration => migration.MigrationsAssembly(typeof(Program).Assembly.FullName));
                optionsAction.UseLazyLoadingProxies();
            });

            builder.Services.ConfigureDbInitializer();
            builder.Services.AddIdentities();
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddMappers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Game");
                    options.DocExpansion(DocExpansion.List);
                    options.OAuthClientId("Api");
                    options.OAuthClientSecret("client_secret");
                });
                app.DbInitialize();
            }

            app.UseMiddleware<ExceptionHandler>();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}