using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WattEsportsCore.Areas.Identity.IdentityHostingStartup))]
namespace WattEsportsCore.Areas.Identity
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