
using System.Collections.Generic;
using System.Threading.Tasks;
using NewspaperAdvertisementManagementSystem.Models;
using NewspaperAdvertisementManagementSystem.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AdvertisementRepository(ApplicationDbContext context, UserManager<Client> userManager, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this._context = context;
            this._hostEnvironment = hostEnvironment;
        }
        public async Task<List<Advertisement>> GetAdvertisements()
        {
            List<Advertisement> advertisementsList;

            advertisementsList = await _context.Advertisements.Include(i => i.Client).ToListAsync();

            return advertisementsList;

        }

        public async Task<int> AddAdvertisement(Advertisement advertisement)
        {
            string userName = httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;

            var client = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == userName);

            advertisement.ClientId = client.Id;

            advertisement.AdvertisementImageName = await SaveImage(advertisement.AdvertisementImageFile);


            var result = await _context.Advertisements.AddAsync(advertisement);

            await _context.SaveChangesAsync();

            return Convert.ToInt32(result.Entity.AdvertisementId);
        }


        public async Task<string> SaveImage(IFormFile imageFile)
        {
            // System.IO.File.WriteAllText("output.txt", imageFile.ToString());
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);


            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;



        }
        public async Task<Advertisement> UpdateAdvertisementStatus(int AdvertisementId)
        {
            var result = await _context.Advertisements.FirstOrDefaultAsync(adv => adv.AdvertisementId == AdvertisementId);

            if (result != null)
            {
                result.AdminApproved = true;

                result.AdvPublishedDate = Convert.ToDateTime(DateTime.Now);
                result.Notifications = true;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<List<Advertisement>> GetExpiredAdvertisements()
        {

            var result = await _context.Advertisements.Where(x => x.AdvPublishedDate.AddDays(x.SubscriptionDays) < DateTime.Today && x.AdminApproved == true).ToListAsync();

            return result;
        }

        public async Task<List<Advertisement>> GetAdvertisementsByClientId()
        {

            string userName = httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;

            var client = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == userName);
            var result = await _context.Advertisements.Where(x => x.ClientId == client.Id).ToListAsync();

            return result;

        }

        public async Task<string> DeleteAdvertisement(int AdvertisementId)
        {
            var result = await _context.Advertisements.FirstOrDefaultAsync(x => x.AdvertisementId == AdvertisementId);

            if (result != null)
            {
                _context.Advertisements.Remove(result);

                await _context.SaveChangesAsync();

                return "SuccessfullyDeleted";

            }
            return null;

        }



        public async Task<Advertisement> UpdateAdvertisementByClient(Advertisement advertisement)
        {
            var result = await _context.Advertisements.FirstOrDefaultAsync(x => x.AdvertisementId == advertisement.AdvertisementId);

            if (result != null)
            {
                result.AdvertisementType = advertisement.AdvertisementType;
                result.AdvertisementTitle = advertisement.AdvertisementTitle;
                result.AdvertisementDesc = advertisement.AdvertisementDesc;
                deleteImage(result.AdvertisementImageName);
                result.AdvertisementImageName = await SaveImage(advertisement.AdvertisementImageFile);
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

        public async Task<string> DeleteAdvertisementByClient(int AdvertisementId)
        {
            string userName = httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;

            var client = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == userName);
            var result = await _context.Advertisements.FirstOrDefaultAsync(x => (x.ClientId == client.Id) && (x.AdvertisementId == AdvertisementId));


            if (result != null)
            {

                deleteImage(result.AdvertisementImageName);
                _context.Advertisements.Remove(result);

                await _context.SaveChangesAsync();

                return "SuccessfullyDeleted";

            }
            return null;
        }

        public async Task<List<Advertisement>> GetUnApprovedAds()
        {
            var result = await _context.Advertisements.Where(x => x.AdminApproved == false).ToListAsync();
            return result;
        }

        public async Task<string> DeleteNotification(int AdvertisementId)
        {
            var result = await _context.Advertisements.FirstOrDefaultAsync(x => x.AdvertisementId == AdvertisementId && x.Notifications == true);
            if (result != null)
            {
                result.Notifications = false;
                await _context.SaveChangesAsync();

                return "success";
            }
            return null;
        }
    }
}