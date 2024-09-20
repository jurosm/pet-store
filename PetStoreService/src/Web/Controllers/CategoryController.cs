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
        private readonly CategoryService _categoryService = categoryService;

        [HttpGet]
        public IEnumerable<CategoryUnit> GetAll()
        {
            return _categoryService.GetAll();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
        }

        [Authorize]
        [HttpPost]
        public async Task Add([FromBody] CategoryUpdateRequest name)
        {
            await _categoryService.AddAsync(name);
        }

        [Authorize]
        [HttpPost("edit/{id}")]
        public async Task Edit(int id, [FromBody] CategoryUpdateRequest name)
        {
            await _categoryService.EditAsync(id, name);
        }
    }
}