using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Toy;
using PetStore.API.Models.Response;
using PetStore.API.Models.Response.Toy;
using PetStore.API.Services.ToySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Controllers
{
    [Route("/toys")]
    public class ToyController : BaseApiController
    {
        ToyService ToyService;
        public ToyController(ToyService toyService)
        {
            this.ToyService = toyService;
        }

        [HttpGet]
        public ToysResponse GetToysPage([FromQuery]int page = 1, [FromQuery] int order = 0, [FromQuery] string match = "", [FromQuery] int categoryId = 0, [FromQuery] int pageSize = 5)
        {
            return ToyService.GetToysPage(pageSize, page, order, match, categoryId);
        }

        [HttpGet("{id}")]
        public ToyResponse GetToy(int id)
        {
            return ToyService.GetToy(id);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteToy(int id)
        {
            await ToyService.DeleteToyAsync(id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task UpdateToy([FromBody] ToyData toy, int id)
        {
            await ToyService.UpdateToyAsync(toy, id);
        }

        [HttpPost]
        [Authorize]
        public async Task AddToy([FromBody] ToyData toyUnit)
        {
            await ToyService.AddToyAsync(toyUnit);
        }

    }
}
