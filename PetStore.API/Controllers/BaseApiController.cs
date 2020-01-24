using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
