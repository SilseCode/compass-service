using Compass.Site.Database;
using Compass.Site.Services;
using CompassSite.Database.Contexts;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using CompassSite.Database.Repositories;
using CompassSite.Services;
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
            string connectionString = null;
#if DEBUG
            connectionString = Configuration[$"ConnectionStrings:{dbProvider}"];
#else
            connectionString = Configuration["ConnectionString"];
#endif
            switch (dbProvider)
            {
                case "Postgres":
                    {
                        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Transient);
                        break;
                    }
                case "MySql":
                    {
                        services.AddDbContext<DatabaseContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);
                        break;
                    }
                case "MsSql":
                    {
                        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
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
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
