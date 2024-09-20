using PetStore.API.Db;
using PetStore.API.Services.CRUD;

namespace PetStore.API.Services.CategorySystem
{
    public class CategoryRepository(ContextWrapper<Category> context) : Repository<Category>(context)
    {
    }
}