using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PetStoreService.Web.Filters.GlobalFilters;
using PetStoreService.Web.Helper;

namespace PetStoreService.Web.Controllers
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