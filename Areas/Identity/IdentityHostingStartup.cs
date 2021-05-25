using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(InstaTicket.Areas.Identity.IdentityHostingStartup))]
namespace InstaTicket.Areas.Identity
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