using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookShop.Areas.Identity.Services;
using BookShop.Classes;
using BookShop.Models;
using BookShop.Models.Repository;
using BookShop.Models.UnitOfWork;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using ReflectionIT.Mvc.Paging;

namespace BookShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ConvertDate>();
            services.AddTransient<IConvertDate, ConvertDate>();
            services.AddTransient<BooksRepository>();
            services.AddTransient<BookShopContext>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<ApplicationIdentityErrorDescriber>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ISmsSender, SmsSender>();
            services.AddHttpClient();

            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddMvc(options =>
            {
                var F = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var L = F.Create("ModelBindingMessages", "BookShop");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                 (x) => L["انتخاب یکی از موارد لیست الزامی است."]);

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });

            services.AddAuthentication()
              .AddGoogle(options =>
              {
                  options.ClientId = "315654760867-d01fsd0fb847vft0fbo6hvbgqghrt5ph.apps.googleusercontent.com";
                  options.ClientSecret = "F7rY4md1LciG24O_4J_RAPct";
              })
                .AddYahoo(options =>
                {
                    options.ClientId = "dj0yJmk9aWxnZVZNTGVwVXhWJnM9Y29uc3VtZXJzZWNyZXQmc3Y9MCZ4PWQz";
                    options.ClientSecret = "9d68b57943e8035cd0771f49d2b54af10797eb4e";
                });

            services.AddPaging(options =>
{
    options.ViewName = "Bootstrap4";
    options.HtmlIndicatorDown = "<i class='fa fa-sort-amount-down'></i>";
    options.HtmlIndicatorUp = "<i class='fa fa-sort-amount-up'></i>";
});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
            name: "areas",
            template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
