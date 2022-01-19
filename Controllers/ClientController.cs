using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewspaperAdvertisementManagementSystem.Repositories;
using NewspaperAdvertisementManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
namespace NewspaperAdvertisementManagementSystem.Controllers
{
    [Authorize(Roles = UserRoles.User)]
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

        [HttpDelete("DeleteAdvertisementByClient/{AdvertisementId}")]
        public async Task<IActionResult> DeleteAdvertisementByClient(int AdvertisementId)
        {
            var result = await advertisementRepository.DeleteAdvertisementByClient(AdvertisementId);
            return Ok(result);
        }

        [HttpGet("GetAdvertisementsByClientId")]
        public async Task<IActionResult> GetAdvertisementsByClientId()
        {
            // var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            // Getting the ID value
            // var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name);
            // System.IO.File.WriteAllText("output.txt", claim.Value);

            var advertisementsList = await advertisementRepository.GetAdvertisementsByClientId();

            return Ok(advertisementsList);

        }

        // [HttpPost("AddPayment")]
        // public async Task<IActionResult> AddPayment([FromBody] Payment payment)
        // {
        //     var result = await paymentRepository.AddPayment(payment);

        //     return Ok(result);
        // }

        [HttpPut("UpdateAdvertisement")]
        public async Task<IActionResult> UpdateAdvertisement([FromForm] Advertisement advertisement)
        {
            var result = await advertisementRepository.UpdateAdvertisementByClient(advertisement);
            return Ok(result);
        }

        [HttpPut("DeleteNotification/{AdvertisementId}")]
        public async Task<IActionResult> DeleteNotification(int AdvertisementId)
        {
            var result = await advertisementRepository.DeleteNotification(AdvertisementId);
            return Ok(result);
        }

        // [HttpPost("CreateProfile")]
        // public async Task<IActionResult> CreateProfile([FromForm] Client client)
        // {
        //     var result = await clientRepository.AddClient(client);

        //     return Ok(result);

        // }

        [HttpPut("UpdateProfile")]

        public async Task<IActionResult> UpdateProfile([FromForm] Client client)
        {
            var result = await clientRepository.UpdateClient(client);
            return NoContent();
        }

        [HttpGet("GetClientInfo")]
        public async Task<IActionResult> GetClientInfo()
        {
            var result = await clientRepository.GetClientInfo();
            return Ok(result);
        }
    }
}