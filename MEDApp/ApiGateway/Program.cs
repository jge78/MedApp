using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using MedAppJwtAuthenticationManager;

namespace MEDApp.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddOcelot(builder.Configuration);
            builder.Services.AddCustomJwtAuthentication();

            var app = builder.Build();
            app.UseOcelot();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
