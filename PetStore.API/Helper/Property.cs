using PetStore.API.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace PetStore.API.Helper
{
    public class Property<T> where T:class
    {
        public static T Change(Action<T> action, T entity)
        {
            action(entity);
            return entity;
        }

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
