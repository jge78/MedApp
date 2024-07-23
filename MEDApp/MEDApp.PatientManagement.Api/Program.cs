using MEDApp.PatientManagement.Api.Messaging;

namespace MEDApp.PatientManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<IMessagingService, PatientManagementMessagingServiceRabbitMQ>();

            // Add services to the container.
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
