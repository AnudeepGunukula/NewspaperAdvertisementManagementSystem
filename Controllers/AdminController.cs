using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NewspaperAdvertisementManagementSystem.Models;
using System.Collections.Generic;
using NewspaperAdvertisementManagementSystem.Repositories;
namespace NewspaperAdvertisementManagementSystem.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdvertisementRepository repository;

        public AdminController(IAdvertisementRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetAdvertisements")]
        public async Task<IActionResult> GetAdvertisements()
        {
            List<Advertisement> advertisementList = await repository.GetAdvertisements();
            return Ok(advertisementList);
        }

        [HttpGet("GetExpiredAdvertisements")]
        public async Task<IActionResult> GetExpiredAdvertisements()
        {
            List<Advertisement> expiredAdvertisements;

            expiredAdvertisements = await repository.GetExpiredAdvertisements();
            return Ok(expiredAdvertisements);
        }

        [HttpPut("UpdateAdvertisementStatus")]
        public async Task<IActionResult> UpdateAdvertisementStatus(int AdvertisementId)
        {
            await repository.UpdateAdvertisementStatus(AdvertisementId);
            return NoContent();
        }




    }
}