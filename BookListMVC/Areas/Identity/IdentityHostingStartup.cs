using System;
using BookListMVC.Areas.Identity.Data;
using BookListMVC.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BookListMVC.Areas.Identity.IdentityHostingStartup))]
namespace BookListMVC.Areas.Identity {
  public class IdentityHostingStartup : IHostingStartup {
    public void Configure(IWebHostBuilder builder) {
      builder.ConfigureServices((context, services) => {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(
                context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<AppUser>(options => {
          options.SignIn.RequireConfirmedAccount = false;
          options.Password.RequireUppercase = false;
          options.Password.RequireLowercase = false;
        })
            .AddEntityFrameworkStores<AuthDbContext>();
      });
    }
  }
}