using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NewspaperAdvertisementManagementSystem.Models;

namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUp(SignUp signUpModel);

        Task<IdentityResult> AdminSignUp(SignUp signUpModel);

        Task<string> Login(SignIn signInModel);
    }
}