using System;
using AspNetCore.SEOHelper;
using Compass.Site.Database;
using Compass.Site.Services;
using CompassSite.Database.Contexts;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using CompassSite.Database.Repositories;
using CompassSite.Services;
using EasyData.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace CompassSite
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentity<User, IdentityRole>(setup =>
                {
                    setup.Password.RequiredLength = 8;
                    setup.Password.RequireDigit = false;
                    setup.Password.RequireLowercase = false;
                    setup.Password.RequireUppercase = false;
                    setup.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<DatabaseContext>();
            RegisterDatabaseContext(services);
            services.AddScoped<Initializer>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ProductService>();
            services.AddScoped<ShopCartManager>();
            services.AddSession(session => session.IdleTimeout = new System.TimeSpan(3, 0, 0, 0));
            services.AddMemoryCache();
            
        }

        private void RegisterDatabaseContext(IServiceCollection services)
        {
            string dbProvider = Configuration["DatabaseProvider"];
            string connectionString = Configuration[$"ConnectionStrings:{dbProvider}"];
            switch (dbProvider)
            {
                case "Postgres":
                    {
                        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString, opt=>opt.EnableRetryOnFailure(5)));
                        break;
                    }
                case "MySql":
                    {
                        services.AddDbContext<DatabaseContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
                        break;
                    }
                case "MsSql":
                    {
                        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
                        break;
                    }
            }
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseRobotsTxt(env.ContentRootPath);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

                endpoints.MapEasyData(options =>
                {
                    options.UseDbContext<DatabaseContext>();
                });
            });
        }
    }
}
