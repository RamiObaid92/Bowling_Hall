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
using Bowling_Hall.src.Events;

namespace Bowling_Hall.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Builder pattern används för att konfigurera och bygga en IHost med olika tjänster.
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

                    // Singleton pattern för en enda instans av logger som används i hela applikationen
                    services.AddSingleton<ILoggerFactory, LoggerFactory>();
                    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

                    // DbContext används för att få infomationen för typen av databas som används och anslutningssträngen från appsettings.json
                    services.AddDbContext<AppDbContext>(options => options.UseSqlite(config.GetSection("Database:ConnectionString").Value));

                    // Ny instans skapas för varje scope som används i applikationen
                    services.AddScoped<IRepository<Member>, MemberRepo>();
                    services.AddScoped<IMemberService, MemberService>();

                    // Ny instans skapas för varje match som spelas
                    services.AddScoped<GameEventSystem>();
                    services.AddScoped<ScoreLogger>();

                    services.AddScoped<MatchLogic>();

                    // Singleton används även här för att skapa en instans av AppMain som kör resten av applikationen
                    services.AddSingleton<AppMain>();
                }).Build();

            using (var scope = builder.Services.CreateScope()) 
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    dbContext.Database.EnsureCreated();
                    logger.LogInformation("Anslutning till databas lyckad");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Anslutning till databas misslyckades");
                }
            }

            var app = builder.Services.GetRequiredService<AppMain>();
            app.Run();
        }
    }
}
