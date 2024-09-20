using Microsoft.EntityFrameworkCore;
using PetStore.API.Db;
using System;
using System.Reflection;

namespace PetStore.API.Helper
{
    public class Property<T> where T : class
    {
        public static DbSet<T> AccessOnCompile(PetStoreDBContext context)
        {
            Assembly executing = Assembly.GetExecutingAssembly();
            Type ContextType = executing.GetType("PetStore.API.Db.PetStoreDBContext");
            Type typeParameterType = typeof(T);
            PropertyInfo prop = ContextType.GetProperty(typeParameterType.Name);
            return prop.GetValue(context) as DbSet<T>;
        }
    }
}