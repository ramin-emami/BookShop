using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookShop.Areas.Admin.Data;
using BookShop.Areas.Admin.Services;
using BookShop.Areas.Api.Controllers;
using BookShop.Areas.Api.Swagger;
using BookShop.Areas.Identity.Data;
using BookShop.Areas.Identity.Services;
using BookShop.Classes;
using BookShop.Models;
using BookShop.Models.Repository;
using BookShop.Models.UnitOfWork;
using BookShop.Models.ViewModels;
using BookShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using ReflectionIT.Mvc.Paging;

namespace BookShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly SiteSettings _siteSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddCustomPolicies();
            services.AddCustomIdentityServices(_siteSettings);
            services.AddCustomApplicationServices();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = actionContext =>
            //    {
            //        var errors = actionContext.ModelState
            //        .Where(e => e.Value.Errors.Count() != 0)
            //        .Select(e => e.Value.Errors.First().ErrorMessage).ToList();

            //        return new BadRequestObjectResult(errors);
            //    };
            //});

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue;
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(),
                    new HeaderApiVersionReader("api-version"));

                //options.Conventions.Controller<SampleV1Controller>().HasApiVersion(new ApiVersion(1, 0));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                //options.AccessDeniedPath = "/Home/AccessDenied";
            });

            services.AddSwagger();
            services.AddPaging(options => {
                options.ViewName = "Bootstrap4";
                options.HtmlIndicatorDown = "<i class='fa fa-sort-amount-down'></i>";
                options.HtmlIndicatorUp = "<i class='fa fa-sort-amount-up'></i>";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var cachePeriod = env.IsDevelopment() ? "5" : "605800";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/" + "node_modules",
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CacheFiles")),
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public,max-age={cachePeriod}");
                },
                RequestPath = "/CacheFiles",
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                //OnPrepareResponse = ctx =>
                //{
                //    ctx.Context.Response.Headers.Append("Cache-Control", $"public,max-age={cachePeriod}");
                //},
                //RequestPath="/MyStaticFiles",
            });
            app.UseNodeModules(env.ContentRootPath);
            app.UseCookiePolicy();
            app.UseCustomIdentityServices();
            app.UseSession();
            app.UseSwaggerAndUI();


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
