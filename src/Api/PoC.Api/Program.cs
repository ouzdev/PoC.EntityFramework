using Infrastructure.Core.StartupConfiguration;
using Infrastructure.Extentions;
using MediatR;
using PoC.Api.Middlewares;
using PoC.Application;
using PoC.Infrastructure;
using PoC.Infrastructure.EfCore;

namespace PoC.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = LoggerFactory.Create(config =>
            {
                config.AddConsole();
                config.AddConfiguration(builder.Configuration.GetSection("Logging"));
            }).CreateLogger("Program");


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
            builder.Services.AddMySwagger(builder.Configuration);

            var app = builder.Build();

            // Migrate latest database changes during startup
            app.MigrateDatabase<PoCDbContext>(logger);

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
