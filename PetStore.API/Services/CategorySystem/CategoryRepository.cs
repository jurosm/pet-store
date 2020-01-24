using PetStore.API.Db;
using PetStore.API.Services.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.CategorySystem
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(ContextWrapper<Category> context) : base(context) { }

    }
}
