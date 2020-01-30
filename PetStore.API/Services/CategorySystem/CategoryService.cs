using AutoMapper;
using PetStore.API.Db;
using PetStore.API.Models.Request.Category;
using PetStore.API.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.CategorySystem
{
    public class CategoryService
    {
        IMapper Mapper;
        CategoryRepository CategoryRepository;

        public CategoryService(CategoryRepository categoryRepository, IMapper mapper)
        {
            this.Mapper = mapper;
            this.CategoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryUnit> GetAll()
        {
            return CategoryRepository.ReadAll().Select(x => Mapper.Map<CategoryUnit>(x));
        }

        public async Task AddAsync(CategoryUpdateRequest name)
        {
            await CategoryRepository.CreateAsync(new Category() { Name = name.Name });
        }

        public async Task DeleteAsync(int id)
        {
            await CategoryRepository.DeleteAsync(id);
        }

        public async Task EditAsync(int id, CategoryUpdateRequest request)
        {
            await CategoryRepository.UpdateAsync(new Category() { Name = request.Name, CategoryId = id });
        }
    }
}
