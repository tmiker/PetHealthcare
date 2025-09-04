using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.API.Abstractions;

namespace PetHealthcare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public PetsController(IPetService petService, IAzureBlobStorageService azureBlobStorageService)
        {
            _petService = petService;
            _azureBlobStorageService = azureBlobStorageService;
        }


    }
}
