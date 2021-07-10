using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MsCoreOne.Infrastructure.Identity;
using MsCoreOne.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace MsCoreOne.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    if (context.Database.IsSqlServer()) 
                    {
                        context.Database.Migrate();
                    }                    

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
                    await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((builderContext, config) =>
                    {
                        var env = builderContext.HostingEnvironment;

                        config.SetBasePath(env.ContentRootPath);
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                        config.AddEnvironmentVariables();
                    })
                    .ConfigureLogging((hostingContext, logging) => 
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    }).UseStartup<Startup>();
                });
    }
}
