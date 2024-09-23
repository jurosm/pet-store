using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStoreService.Application.Models.Request.Category;
using PetStoreService.Application.Models.Response.Category;
using PetStoreService.Application.Services.CategorySystem;
using PetStoreService.Domain.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PetStoreService.Web.Controllers
{
    [Route("/category")]
    public class CategoryController(CategoryService categoryService) : BaseApiController
    {
        private readonly CategoryService _categoryService = categoryService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryUnit>>> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Category>> Add([FromBody] CategoryUpdateRequest name)
        {
            return CreatedAtAction(nameof(Add), await _categoryService.AddAsync(name));
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] CategoryUpdateRequest name)
        {
            return Ok(await _categoryService.EditAsync(id, name));
        }
    }
}