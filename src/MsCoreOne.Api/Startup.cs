using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MsCoreOne.Application;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Extensions;
using MsCoreOne.Infrastructure;
using MsCoreOne.Infrastructure.Persistence;
using MsCoreOne.Api.Filters;
using MsCoreOne.Api.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MsCoreOne.Application.Common.Models;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System;

namespace MsCoreOne.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "MsCoreOneOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>()
                .AddUrlGroup(new Uri("https://localhost:5003"), name: "MsCoreOne - Mvc");

            services.RegisterApiVersioning();

            services.AddControllersWithViews(options => 
                options.Filters.Add(new ApiExceptionFilter()))
                .AddNewtonsoftJson();

            services.AddRazorPages();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["Redis:Configuration"];
            });

            services.RegisterSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckReponse
                    {
                        Status = report.Status.ToString(),
                        HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),
                        HealthCheckDuration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseSwagger();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("mscoreone-swagger");
                c.OAuthClientSecret("secret");
                c.OAuthUsePkce();

                // build a swagger endpoint for each discorved API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
