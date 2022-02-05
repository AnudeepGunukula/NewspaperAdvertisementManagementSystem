using System.Threading.Tasks;
using NewspaperAdvertisementManagementSystem.Contexts;
using NewspaperAdvertisementManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClientRepository(ApplicationDbContext context, UserManager<Client> userManager, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment _hostEnvironment)
        {
            this.httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
            this._hostEnvironment = _hostEnvironment;
            this._context = context;
        }

        // public async Task<int> AddClient(Client client)
        // {
        //     client.ProfileImageName = await SaveImage(client.ProfileImageFile);
        //     var result = await _context.Clients.AddAsync(client);

        //     await _context.SaveChangesAsync();
        //     return result.Entity.ClientId;
        // }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;

        }

        public async Task<Client> GetClientById(string ClientId)
        {

            Client client = await _context.Clients.FirstOrDefaultAsync(client => client.Id == ClientId);

            return client;

        }


        public async Task<ClientInfo> GetClientInfo()
        {
            string userName = httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;

            var client = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == userName);

            ClientInfo cinfo = new ClientInfo();
            cinfo.FirstName = client.FirstName;
            cinfo.LastName = client.LastName;
            cinfo.Address = client.Address;
            cinfo.MobileNumber = client.MobileNumber;
            cinfo.ProfileImageName = client.ProfileImageName;
            return (cinfo);
        }

        public async Task<Client> UpdateClient(Client client)
        {
            string userName = httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;

            var clientObj = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == userName);
            var Id = clientObj.Id;

            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (result != null)
            {
                result.FirstName = client.FirstName;
                result.LastName = client.LastName;
                result.Address = client.Address;
                result.MobileNumber = client.MobileNumber;
                if (result.ProfileImageName != null)
                {
                    deleteImage(result.ProfileImageName);
                }
                result.ProfileImageName = await SaveImage(client.ProfileImageFile);

                await _context.SaveChangesAsync();
                return result;
            }
            return null;

        }

        private void deleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", imageName);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

        }
    }
}