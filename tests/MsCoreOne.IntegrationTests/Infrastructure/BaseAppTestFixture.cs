using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using MsCoreOne.Infrastructure.Identity;
using MsCoreOne.Infrastructure.Persistence;
using MsCoreOne.IntegrationTests.Common.Data;
using MsCoreOne.Api;
using System.IO;
using System.Linq;
using System;

namespace MsCoreOne.IntegrationTests.Infrastructure
{
    public class BaseAppTestFixture : WebApplicationFactory<Program>
    {
        private const string AuthenticationScheme = "Test";
        private ServiceProvider _serviceProvider;

        public BaseAppTestFixture()
        {
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((builderContext, config) =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "appsettings.IntegrationTest.json");

                config.AddJsonFile(configPath);
                config.AddEnvironmentVariables();
            });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault
                       (d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>((options, context) =>
                {
                    context.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                _serviceProvider = services.BuildServiceProvider();

                using var scope = _serviceProvider.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var logger = scope.ServiceProvider.GetRequiredService
                             <ILogger<WebApplicationFactory<Program>>>();

                db.Database.EnsureCreated();

                try
                {
                    db.InitializeTestDatabase();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages.Error: { ex.Message}");
                }

                services.AddAuthentication(AuthenticationScheme)
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticateHandler>(AuthenticationScheme, options => { });

                services.AddAuthorization(options =>
                {
                    options.AddPolicy("Bearer", policy =>
                    {
                        policy.AddAuthenticationSchemes(AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                    });
                });
            });

            base.ConfigureWebHost(builder);
        }

        protected override void Dispose(bool disposing)
        {
            if (_serviceProvider != null)
            {
                using var scope = _serviceProvider.CreateScope();

                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureDeleted();
            }
            base.Dispose(disposing);
        }
    }
}
