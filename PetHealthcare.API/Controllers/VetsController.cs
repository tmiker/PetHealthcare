using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.API.Abstractions;

namespace PetHealthcare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetsController : ControllerBase
    {
        private readonly IVetService _vetService;

        public VetsController(IVetService vetService)
        {
            _vetService = vetService;
        }



    }
}
