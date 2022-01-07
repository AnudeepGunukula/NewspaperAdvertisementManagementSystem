
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

namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdvertisementRepository(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
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
            advertisement.AdvertisementImageName = await SaveImage(advertisement.AdvertisementImageFile);
            var result = await _context.Advertisements.AddAsync(advertisement);

            await _context.SaveChangesAsync();

            return result.Entity.AdvertisementId;
        }


        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images//Advertisements", imageName);

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

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<List<Advertisement>> GetExpiredAdvertisements()
        {
            var result = await _context.Advertisements.Where(x => x.AdvPublishedDate.AddMonths(3) < DateTime.Today).ToListAsync();

            return result;
        }

        public async Task<List<Advertisement>> GetAdvertisementsByClientId(int ClientId)
        {

            var result = await _context.Advertisements.Where(x => x.ClientId == ClientId).ToListAsync();

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

    }
}