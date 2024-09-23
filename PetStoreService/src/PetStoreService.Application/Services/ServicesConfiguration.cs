using Microsoft.Extensions.DependencyInjection;
using PetStoreService.Application.Services.CategorySystem;
using PetStoreService.Application.Services.CommentsSystem;
using PetStoreService.Application.Services.OrderSystem;
using PetStoreService.Application.Services.ToySystem;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services;

public static class ServicesConfiguration
{
    public static void AddMyTOCServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryRepository>();
        services.AddScoped<CategoryService>();

        services.AddScoped<ToyRepository>();
        services.AddScoped<ToyService>();

        services.AddScoped<OrderRepository>();
        services.AddScoped<OrderService>();

        services.AddScoped<CommentRepository>();
        services.AddScoped<CommentService>();
    }
}