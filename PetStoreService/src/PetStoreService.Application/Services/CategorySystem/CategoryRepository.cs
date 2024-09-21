using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.CategorySystem;

public class CategoryRepository(ContextWrapper<Category> context) : Repository<Category>(context)
{
}
