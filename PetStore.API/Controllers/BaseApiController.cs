using Microsoft.AspNetCore.Mvc;

namespace PetStore.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {
        }
    }
}
