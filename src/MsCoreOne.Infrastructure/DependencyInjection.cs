using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Common.Interfaces.Repositories;
using MsCoreOne.Infrastructure.Caching;
using MsCoreOne.Infrastructure.Identity;
using MsCoreOne.Infrastructure.Identity.Configuration;
using MsCoreOne.Infrastructure.Persistence;
using MsCoreOne.Infrastructure.Repositories;
using MsCoreOne.Infrastructure.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsCoreOne.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var clientUrls = new Dictionary<string, string>
            {
                ["Swagger"] = configuration["ClientUrl:Swagger"],
                ["Mvc"] = configuration["ClientUrl:Mvc"],
                ["React"] = configuration["ClientUrl:React"]
            };

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("MsCoreOneDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
                .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
                .AddInMemoryClients(InMemoryConfig.GetClients(clientUrls))
                .AddAspNetIdentity<ApplicationUser>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IStorageService, FileStorageService>();

            // Repositories
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddAuthentication()
                .AddLocalApi("Bearer", option =>
                {
                    option.ExpectedScope = "api.mscoreone";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("MsCoreOneOrigins",
                builder =>
                {
                    builder.WithOrigins(clientUrls["Mvc"], clientUrls["React"], clientUrls["Swagger"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.ConfigureApplicationCookie(c =>
            {
                c.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 403;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSingleton<IUriService>(o =>
            {
                return new UriService(clientUrls["Swagger"]);
            });

            services.AddMemoryCache();
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();

            return services;
        }
    }
}
