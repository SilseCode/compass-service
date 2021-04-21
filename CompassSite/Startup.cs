using Compass.DataAccessLayer.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllersWithViews();
            RegisterDatabaseContext(services);
        }

        private void RegisterDatabaseContext(IServiceCollection services)
        {
            string dbProvider = Configuration["DatabaseProvider"];
            string connectionString = Configuration[$"ConnectionStrings:{dbProvider}"]; 
            switch (dbProvider)
            {
                case "Postgres":
                    {
                        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
                        break;
                    }
                case "MySql":
                    {
                        services.AddDbContext<DatabaseContext>(options => options.UseMySql(connectionString, ServerVersion.FromString(connectionString)));
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
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
