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
        private readonly IAdvertisementRepository advertisementRepository;
        private readonly IPaymentRepository paymentRepository;

        public AdminController(IAdvertisementRepository advertisementRepository, IPaymentRepository paymentRepository)
        {
            this.advertisementRepository = advertisementRepository;
            this.paymentRepository = paymentRepository;
        }

        [HttpGet("GetAdvertisements")]
        public async Task<IActionResult> GetAdvertisements()
        {
            List<Advertisement> advertisementList = await advertisementRepository.GetAdvertisements();
            if (advertisementList != null)
            {
                return Ok(advertisementList);
            }
            return Ok("No Active advertisements");
        }

        [HttpGet("GetExpiredAdvertisements")]
        public async Task<IActionResult> GetExpiredAdvertisements()
        {
            List<Advertisement> expiredAdvertisements;

            expiredAdvertisements = await advertisementRepository.GetExpiredAdvertisements();
            if (expiredAdvertisements != null)
            {
                return Ok(expiredAdvertisements);
            }
            return Ok("No Expired Advertisements");
        }

        [HttpPut("UpdateAdvertisementStatus")]
        public async Task<IActionResult> UpdateAdvertisementStatus(int AdvertisementId)
        {
            var result = await advertisementRepository.UpdateAdvertisementStatus(AdvertisementId);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound();

        }

        [HttpDelete("DeleteAdvertisement")]
        public async Task<IActionResult> DeleteAdvertisement(int AdvertisementId)
        {
            var result = await advertisementRepository.DeleteAdvertisement(AdvertisementId);
            if (result == "SuccessfullyDeleted")
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("GetPaymentByAdvertisementId")]
        public async Task<IActionResult> GetPaymentByAdvertisementId(int AdvertisementId)
        {
            var result = await paymentRepository.GetPaymentByAdvertisementId(AdvertisementId);

            return Ok(result);
        }





    }
}