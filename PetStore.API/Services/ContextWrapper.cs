using PetStore.API.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetStore.API.Helper;

namespace PetStore.API.Services.CRUD
{

    public class ContextWrapper<T> where T : class
    {
        public readonly PetStoreDBContext PSContext;

        public ContextWrapper(PetStoreDBContext blogContext)
        {
            Table = Property<T>.AccessOnCompile(blogContext);
            PSContext = blogContext;
        }

        public DbSet<T> Table { get; set; }

        public async Task SaveChangesAsync()
        {
            await PSContext.SaveChangesAsync();
        }

        internal void SaveChanges()
        {
            PSContext.SaveChanges();
        }
    }
}
