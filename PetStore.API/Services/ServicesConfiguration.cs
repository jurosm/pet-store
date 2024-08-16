using Microsoft.Extensions.DependencyInjection;
using PetStore.API.Db;
using PetStore.API.Services.CategorySystem;
using PetStore.API.Services.CommentsSystem;
using PetStore.API.Services.CRUD;
using PetStore.API.Services.OrderSystem;
using PetStore.API.Services.ToySystem;

namespace PetStore.API.Services
{
    public static class ServicesConfiguration
    {
        public static void AddMyTOCServices(this IServiceCollection services)
        {
            services.AddScoped<ContextWrapper<Category>>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<CategoryService>();

            services.AddTransient<ContextWrapper<Toy>>();
            services.AddScoped<ToyRepository>();
            services.AddScoped<ToyService>();

            services.AddTransient<ContextWrapper<Order>>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<OrderService>();

            services.AddTransient<ContextWrapper<Comment>>();
            services.AddScoped<CommentRepository>();
            services.AddScoped<CommentService>();
        }
    }
}
