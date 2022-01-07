using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewspaperAdvertisementManagementSystem.Repositories;
using NewspaperAdvertisementManagementSystem.Models;
namespace NewspaperAdvertisementManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IAdvertisementRepository repository;

        public ClientController(IAdvertisementRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("AddAdvertisement")]
        public async Task<IActionResult> AddAdvertisement(Advertisement advertisement)
        {
            var id = await repository.AddAdvertisement(advertisement);
            return Ok(id);
            // return CreatedAtAction(nameof(GetEmployeeById), new { id = id, controller = "Employee" }, employee);
        }

        [HttpGet("GetAdvertisementsByClientId")]
        public async Task<IActionResult> GetAdvertisementsByClientId(int ClientId)
        {
            var advertisementsList = await repository.GetAdvertisementsByClientId(ClientId);
            return Ok(advertisementsList);

        }


    }
}