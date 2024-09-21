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
    [Route("/toy")]
    public class ToyController(ToyService toyService) : BaseApiController
    {
        private readonly ToyService _toyService = toyService;

        [HttpGet]
        public ActionResult<ToysResponse> GetToysPage([FromQuery] int? categoryId, [FromQuery] string match, [FromQuery] int page = 1, [FromQuery] ToyOrder order = 0, [FromQuery] int pageSize = 5)
        {
            return Ok(_toyService.GetToysPage(pageSize, page, order, match, categoryId));
        }

        [HttpGet("{id}")]
        public ActionResult<ToyResponse> GetToy(int id)
        {
            return Ok(_toyService.GetToy(id));
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