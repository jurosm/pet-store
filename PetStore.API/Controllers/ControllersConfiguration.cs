using PetStore.API.Filters.GlobalFilters;
using PetStore.API.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace PetStore.API.Controllers
{
    public static class ControllersConfiguration
    {
        public static void AddMyControllersServices(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ExceptionActionFilter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    ModelErrorResponse mer = new(context.ModelState);
                    context.HttpContext.Response.StatusCode = 422;
                    return new JsonResult(mer);
                };
            });
        }
    }
}
