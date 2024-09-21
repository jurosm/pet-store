using Microsoft.AspNetCore.Mvc;

namespace PetStoreService.Web.Controllers
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