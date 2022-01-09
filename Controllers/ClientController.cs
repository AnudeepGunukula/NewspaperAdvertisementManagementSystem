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
        private readonly IClientRepository clientRepository;

        public ClientController(IAdvertisementRepository advertisementRepository, IPaymentRepository paymentRepository, IClientRepository clientRepository)
        {
            this.advertisementRepository = advertisementRepository;
            this.paymentRepository = paymentRepository;
            this.clientRepository = clientRepository;
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

        [HttpPut("UpdateAdvertisement")]
        public async Task<IActionResult> UpdateAdvertisement([FromForm] Advertisement advertisement)
        {
            var result = await advertisementRepository.UpdateAdvertisementByClient(advertisement);
            return Ok(result);
        }

        [HttpPost("CreateProfile")]
        public async Task<IActionResult> CreateProfile([FromForm] Client client)
        {
            var result = await clientRepository.AddClient(client);

            return Ok(result);

        }

        [HttpPut("UpdateProfile")]

        public async Task<IActionResult> UpdateProfile([FromForm] Client client)
        {
            var result = await clientRepository.UpdateClient(client);
            return Ok(result);
        }
    }
}