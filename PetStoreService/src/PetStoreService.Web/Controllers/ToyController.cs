using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStoreService.Application.Models.Request.Toy;
using PetStoreService.Application.Models.Response.Toy;
using PetStoreService.Application.Services.ToySystem;
using PetStoreService.Domain.Entities;
using System.Net;
using System.Threading.Tasks;

namespace PetStoreService.Web.Controllers
{
    [Route("toy")]
    public class ToyController(ToyService toyService) : BaseApiController
    {
        private readonly ToyService _toyService = toyService;

        [HttpGet]
        public async Task<ActionResult<ToysResponse>> GetToysPage([FromQuery] int? categoryId, [FromQuery] string match, [FromQuery] int offset = 1, [FromQuery] ToyOrder order = 0, [FromQuery] int limit = 5)
        {
            return Ok(await _toyService.GetToysPageAsync(limit, offset, order, match, categoryId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToyResponse>> GetToy(int id)
        {
            return Ok(await _toyService.GetToyAsync(id));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteToy(int id)
        {
            await _toyService.DeleteToyAsync(id);
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Toy>> UpdateToy([FromBody] ToyData toy, int id)
        {
            return Ok(await _toyService.UpdateToyAsync(toy, id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Toy>> AddToy([FromBody] ToyData toyUnit)
        {
            return CreatedAtAction(nameof(AddToy), await _toyService.AddToyAsync(toyUnit));
        }
    }
}