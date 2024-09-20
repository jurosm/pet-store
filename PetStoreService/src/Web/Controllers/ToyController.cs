using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Toy;
using PetStore.API.Models.Response.Toy;
using PetStore.API.Services.ToySystem;
using System.Threading.Tasks;

namespace PetStore.API.Controllers
{
    [Route("/toys")]
    public class ToyController(ToyService toyService) : BaseApiController
    {
        private readonly ToyService _toyService = toyService;

        [HttpGet]
        public ToysResponse GetToysPage([FromQuery] int? categoryId, [FromQuery] string match, [FromQuery] int page = 1, [FromQuery] ToyOrder order = 0, [FromQuery] int pageSize = 5)
        {
            return _toyService.GetToysPage(pageSize, page, order, match, categoryId);
        }

        [HttpGet("{id}")]
        public ToyResponse GetToy(int id)
        {
            return _toyService.GetToy(id);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteToy(int id)
        {
            await _toyService.DeleteToyAsync(id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task UpdateToy([FromBody] ToyData toy, int id)
        {
            await _toyService.UpdateToyAsync(toy, id);
        }

        [HttpPost]
        [Authorize]
        public async Task AddToy([FromBody] ToyData toyUnit)
        {
            await _toyService.AddToyAsync(toyUnit);
        }
    }
}