using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("")]
        public ToysResponse GetToysPage([FromQuery]int page=1, [FromQuery] string order = "asc", [FromQuery] string match = "", [FromQuery] string category = "", [FromQuery] int pageSize = 5)
        {
            return ToyService.GetToysPage(pageSize, page, order, match, category);
        }
        
        [HttpGet("{id}")]
        public ToyResponse GetToy(int id)
        {
            return ToyService.GetToy(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteToy(int id)
        {
            await ToyService.DeleteToyAsync(id);
        }

        [HttpPost("edit/{id}")]
        public async Task UpdateToy([FromBody] ToyChangeRequest toy, int id)
        {
            toy.ToyId = id;
            await ToyService.UpdateToyAsync(toy);
        }

        [HttpPost("add")]
        public async Task AddToy([FromBody] ToyUnit toyUnit)
        {
            await ToyService.AddToyAsync(toyUnit);
        }
        
    }
}
