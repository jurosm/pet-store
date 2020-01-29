using Microsoft.AspNetCore.Mvc;
using PetStore.API.Db;
using PetStore.API.Models.Request.Category;
using PetStore.API.Models.Response.Category;
using PetStore.API.Services.CategorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Controllers
{
    [Route("/categories")]
    public class CategoryController : BaseApiController
    {
        CategoryService CategoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.CategoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<CategoryUnit> GetAll()
        {
            return CategoryService.GetAll();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await CategoryService.DeleteAsync(id);
        }

        [HttpPost]
        public async Task Add([FromBody] CategoryUpdateRequest name)
        {
            await CategoryService.AddAsync(name);
        }

        [HttpPost("edit/{id}")]
        public async Task Edit(int id, [FromBody] CategoryUpdateRequest name)
        {
            await CategoryService.EditAsync(id, name);
        }
    }
}
