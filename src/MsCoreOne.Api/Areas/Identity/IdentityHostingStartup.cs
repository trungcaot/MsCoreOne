using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MsCoreOne.Areas.Identity.IdentityHostingStartup))]
namespace MsCoreOne.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}