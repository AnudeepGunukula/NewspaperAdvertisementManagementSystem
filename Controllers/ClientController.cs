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
        private readonly IAdvertisementRepository advertisementRepository;
        private readonly IPaymentRepository paymentRepository;

        public ClientController(IAdvertisementRepository advertisementRepository, IPaymentRepository paymentRepository)
        {
            this.advertisementRepository = advertisementRepository;
            this.paymentRepository = paymentRepository;
        }

        [HttpPost("AddAdvertisement")]
        public async Task<IActionResult> AddAdvertisement([FromForm] Advertisement advertisement)
        {
            var id = await advertisementRepository.AddAdvertisement(advertisement);
            return Ok(id);
            // return CreatedAtAction(nameof(GetEmployeeById), new { id = id, controller = "Employee" }, employee);
        }

        [HttpGet("GetAdvertisementsByClientId")]
        public async Task<IActionResult> GetAdvertisementsByClientId(int ClientId)
        {
            var advertisementsList = await advertisementRepository.GetAdvertisementsByClientId(ClientId);
            return Ok(advertisementsList);

        }

        [HttpPost("AddPayment")]
        public async Task<IActionResult> AddPayment(Payment payment)
        {
            var result = await paymentRepository.AddPayment(payment);

            return Ok(result);
        }


    }
}