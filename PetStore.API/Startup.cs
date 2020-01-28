using CodeSpaceBlog.API.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetStore.API.Controllers;
using PetStore.API.Filters.GlobalFilters;
using PetStore.API.Services;
using PetStore.API.Services.AuthenticationSystem;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetStore.API.Services.ExternalServices;
using System;

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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddTransient<EnvironmentConfigurationService>();

            services.AddScoped<ExceptionActionFilter>();

            services.AddExternalServices(Environment.GetEnvironmentVariable("STRIPE_SECRET"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(Startup));

            services.AddCors();

            services.AddPetStoreDBServices(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));

            services.AddMyTOCServices();

            services.AddAuth0AuthenticationServices(Environment.GetEnvironmentVariable("AUTH0_DOMAIN"), Environment.GetEnvironmentVariable("AUTH0_AUDIENCE"));

            services.AddAuthorization();

            services.AddMyControllersServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            // app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

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
