using MEDApp.UserManagement.Api.Controllers;
using MEDApp.UserManagement.Api.Messaging;
namespace MEDApp.UserManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddSingleton<IMessagingService, RabbitMQMessagingService>();
            builder.Services.AddTransient<IMessagingService, RabbitMQMessagingService>();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.MapControllers();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
