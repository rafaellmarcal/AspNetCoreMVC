using AspNetCoreMVC.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreMVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (environment.IsProduction())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration(Configuration);

            services.AddAuthorizationConfiguration();

            services.ResolveDependencies();

            services.AddLoggerConfiguration();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error/500");
                app.UseStatusCodePagesWithRedirects("/error/{0}");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseLoggerConfiguration(Configuration);

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapAreaControllerRoute("IdentityArea", "Identity", "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
