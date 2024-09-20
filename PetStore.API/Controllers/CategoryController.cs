using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Category;
using PetStore.API.Models.Response.Category;
using PetStore.API.Services.CategorySystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.API.Controllers
{
    [Route("/categories")]
    public class CategoryController(CategoryService categoryService) : BaseApiController
    {
        private readonly CategoryService CategoryService = categoryService;

        [HttpGet]
        public IEnumerable<CategoryUnit> GetAll()
        {
            return CategoryService.GetAll();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await CategoryService.DeleteAsync(id);
        }

        [Authorize]
        [HttpPost]
        public async Task Add([FromBody] CategoryUpdateRequest name)
        {
            await CategoryService.AddAsync(name);
        }

        [Authorize]
        [HttpPost("edit/{id}")]
        public async Task Edit(int id, [FromBody] CategoryUpdateRequest name)
        {
            await CategoryService.EditAsync(id, name);
        }
    }
}