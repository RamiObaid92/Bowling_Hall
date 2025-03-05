using Bowling_Hall.src.Data;
using Bowling_Hall.src.App;
using Bowling_Hall.src.Interfaces;
using Bowling_Hall.src.Models;
using Bowling_Hall.src.Repositories;
using Bowling_Hall.src.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bowling_Hall.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureServices((context, services) =>
                {
                    var config = context.Configuration;

                    services.AddSingleton<ILoggerFactory, LoggerFactory>();
                    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

                    services.AddDbContext<AppDbContext>(options => options.UseSqlite(config.GetSection("Database:ConnectionString").Value));

                    services.AddScoped<IRepository<Member>, MemberRepo>();
                    services.AddScoped<IMemberService, MemberService>();

                    services.AddSingleton<AppMain>();
                }).Build();

            using (var scope = builder.Services.CreateScope()) 
            {
                // Logger brought into scope
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                // get the current directory
                var currentDirectory = Directory.GetCurrentDirectory();

                // Log the directory
                logger.LogInformation("Current working directory: {Directory}", currentDirectory);
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }

            var app = builder.Services.GetRequiredService<AppMain>();
            app.Run();
        }
    }
}
