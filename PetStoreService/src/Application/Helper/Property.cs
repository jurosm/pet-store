﻿using Microsoft.EntityFrameworkCore;
using PetStoreService.Persistence;
using System.Reflection;

namespace PetStoreService.Application
{
    public class Property<T> where T : class
    {
        public static DbSet<T> AccessOnCompile(PetStoreDBContext context)
        {
            Type ContextType = Type.GetType("PetStoreService.Persistence.PetStoreDBContext, Persistence");
            Type typeParameterType = typeof(T);
            PropertyInfo prop = ContextType.GetProperty(typeParameterType.Name);
            return prop.GetValue(context) as DbSet<T>;
        }
    }
}