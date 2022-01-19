using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewspaperAdvertisementManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<Client> userManager, SignInManager<Client> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this._roleManager = roleManager;
            this._configuration = configuration;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }


        public async Task<IdentityResult> SignUp(SignUp signUpModel)
        {
            var user = new Client()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.LastName,
                UserName = signUpModel.Email,
                SecurityQuestion = signUpModel.SecurityQuestion
            };

            var result = await _userManager.CreateAsync(user, signUpModel.Password);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User);

            return result;


        }


        public async Task<IdentityResult> AdminSignUp(SignUp signUpModel)
        {
            var user = new Client()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.LastName,
                UserName = signUpModel.Email
            };

            var result = await _userManager.CreateAsync(user, signUpModel.Password);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return result;


        }

        public async Task<Response> Login(SignIn signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);
            // 

            if (!result.Succeeded)
            {
                return null;
            }

            var users = await _userManager.FindByNameAsync(signInModel.Email);
            var roles = await _userManager.GetRolesAsync(users);

            var role = roles[0];


            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name,signInModel.Email),
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
               new Claim("role",role)

        };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            Response response = new Response();

            response.Token = jwtToken;
            response.role = role;

            return response;

        }




        public async Task<ForgotPasswordToken> ForgotPassword(ForgotPassword model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null && user.SecurityQuestion == model.SecurityQuestion)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                ForgotPasswordToken forgotPasswordObj = new ForgotPasswordToken();

                forgotPasswordObj.Email = model.Email;
                forgotPasswordObj.Token = token;

                return forgotPasswordObj;
            }
            return null;
        }


        public async Task<bool> ResetPassword(ResetPasswordModel resetmodel)
        {
            if (resetmodel.Token == null || resetmodel.Email == null)
            {
                return false;
            }

            var user = await _userManager.FindByNameAsync(resetmodel.Email);

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetmodel.Token, resetmodel.Password);

            if (!resetPassResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}