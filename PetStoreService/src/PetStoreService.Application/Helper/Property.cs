using Microsoft.EntityFrameworkCore;
using PetStoreService.Persistence;
using System.Reflection;

namespace PetStoreService.Application.Helper
{
    public class Property<T> where T : class
    {
        public static DbSet<T>? AccessOnCompile(PetStoreDBContext context)
        {
            Type? ContextType = Type.GetType("PetStoreService.Persistence.PetStoreDBContext, PetStoreService.Persistence");
            Type typeParameterType = typeof(T);
            PropertyInfo? prop = ContextType!.GetProperty(typeParameterType.Name);
            return prop?.GetValue(context) as DbSet<T>;
        }
    }
}