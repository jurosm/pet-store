
using PetStore.API.Services.CRUD;
using PetStoreService.Domain.Entities;

namespace PetStore.API.Services.CategorySystem
{
    public class CategoryRepository(ContextWrapper<Category> context) : Repository<Category>(context)
    {
    }
}