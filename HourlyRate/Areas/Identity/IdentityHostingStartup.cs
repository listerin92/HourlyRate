using HourlyRate.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace HourlyRate.Areas.Identity
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
