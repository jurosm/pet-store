using PetStore.API.Db;
using PetStore.API.Models.Request.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.CategorySystem
{
    public class CategoryService
    {
        CategoryRepository CategoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this.CategoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAll()
        {
            return CategoryRepository.ReadAll();
        }

        public async Task AddAsync(string name)
        {
            await CategoryRepository.CreateAsync(new Category() {Name = name});
        }

        public async Task DeleteAsync(int id)
        {
            await CategoryRepository.DeleteAsync(id);
        }

        public async Task EditAsync(int id, CategoryUpdateRequest request)
        {
            await CategoryRepository.UpdateAsync(new Category() { Name = request.Name, CategoryId = id});
        }
    }
}
