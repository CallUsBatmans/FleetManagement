using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FleetManagement.IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "http://localhost:5006";
                    options.UserInteraction.ErrorUrl = "http://localhost:5006/error";
                    options.UserInteraction.LogoutUrl = "http://localhost:5006/logout";
                })
                .AddInMemoryApiResources(Configuration.GetApis())
                .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
                .AddInMemoryClients(Configuration.GetClients())
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
