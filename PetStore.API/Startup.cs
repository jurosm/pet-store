using CodeSpaceBlog.API.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetStore.API.Controllers;
using PetStore.API.Db;
using PetStore.API.Filters.GlobalFilters;
using PetStore.API.Services;
using PetStore.API.Services.AuthenticationSystem;

namespace PetStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ExceptionActionFilter>();

            services.AddPetStoreDBServices(Configuration.GetConnectionString("DefaultConnection"));

            services.AddMyTOCServices();

            services.AddAuth0AuthenticationServices(Configuration["Auth0:Domain"], Configuration["Auth0:Audience"]);

            services.AddAuthorization();

            services.AddMyControllersServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
