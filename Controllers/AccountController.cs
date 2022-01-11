using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewspaperAdvertisementManagementSystem.Models;
using NewspaperAdvertisementManagementSystem.Repositories;

namespace NewspaperAdvertisementManagementSystem.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUp signUpModel)
        {
            var result = await _accountRepository.SignUp(signUpModel);

            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();

        }



        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("AdminSignUp")]
        public async Task<IActionResult> AdminSignUp([FromBody] SignUp signUpModel)
        {
            var result = await _accountRepository.AdminSignUp(signUpModel);

            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();

        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] SignIn signInModel)
        {
            var result = await _accountRepository.Login(signInModel);
            if (result == null)
            {
                return Unauthorized();

            }
            return Ok(result);

        }


    }
}