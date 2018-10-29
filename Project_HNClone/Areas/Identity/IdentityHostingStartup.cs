using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project_HNClone.Areas.Identity.Data;

[assembly: HostingStartup(typeof(Project_HNClone.Areas.Identity.IdentityHostingStartup))]
namespace Project_HNClone.Areas.Identity
{
  public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Project_HNCloneIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<Project_HNCloneUser>()
                  .AddEntityFrameworkStores<Project_HNCloneIdentityDbContext>();                                    
            });
        }
    }
}