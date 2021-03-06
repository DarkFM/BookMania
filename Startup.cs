﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMania.Core.Entities.UserAggregate;
using BookMania.Core.Interfaces;
using BookMania.Extensions;
using BookMania.Infrastructure.Data;
using BookMania.Interfaces;
using BookMania.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookMania
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CatalogContext"));
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            services.AddIdentity<ApplicationUser, IdentityRole<int>>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 4;
                opts.Password.RequireDigit = false;
                opts.Password.RequireUppercase = false;

                const string letters = "abcdefghijklmnopqrstuvwxyz";
                opts.User.AllowedUserNameCharacters = letters + letters.ToUpperInvariant() + "-_,+";
            })
                .AddEntityFrameworkStores<CatalogContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.Cookie.MaxAge = TimeSpan.FromDays(1);
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICatalogViewModelService, CatalogViewModelService>();
            services.AddScoped<IBookDetailsService, BookDetailsSerivce>();
            services.AddScoped<IUserService, UserService>();

            services.AddGoogleBooks(Configuration);

            //services.AddHttpContextAccessor();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // To trigger browswer link on code restart
            try
            {
                System.IO.File.WriteAllText("browsersync-update.txt", DateTime.Now.ToString());
            }
            catch
            {}
        }
    }
}
