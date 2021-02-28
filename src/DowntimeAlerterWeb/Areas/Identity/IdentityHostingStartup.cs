using DowntimeAlerterWeb.Areas.Identity.Data;
using DowntimeAlerterWeb.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DowntimeAlerterWeb.Areas.Identity.IdentityHostingStartup))]
namespace DowntimeAlerterWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DowntimeAlerterWebContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("DowntimeAlerterWebContextConnection")));

                services.AddDefaultIdentity<DowntimeAlerterWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<DowntimeAlerterWebContext>();
            });
        }
    }
}