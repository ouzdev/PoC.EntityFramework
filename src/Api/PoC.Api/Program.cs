using Infrastructure.Core.StartupConfiguration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoC.Application;
using PoC.Infrastructure.EfCore;

namespace PoC.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<PoCDbContext>(opt =>
            {
                opt.UseNpgsql(builder.Configuration.GetConnectionString("PoC"));
            });
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
            builder.Services.AddMySwagger(builder.Configuration);
            builder.Services.AddApplication();

            var app = builder.Build();


            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
