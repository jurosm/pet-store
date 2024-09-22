using PetStoreService.Domain.Entities;
using PetStoreService.Persistence;

namespace PetStoreService.Application.Services.CategorySystem;

public class CategoryRepository(PetStoreDBContext context) : Repository<Category>(context)
{
}