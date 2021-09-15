using System;
using BookShop.Areas.Identity.Data;
using BookShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BookShop.Areas.Identity.IdentityHostingStartup))]
namespace BookShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityDBContextConnection")));

                services.AddDefaultIdentity<BookShopUser>()
                    .AddEntityFrameworkStores<IdentityDBContext>();
            });
        }
    }
}