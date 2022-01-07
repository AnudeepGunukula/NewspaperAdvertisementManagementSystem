
using System.Collections.Generic;
using System.Threading.Tasks;
using NewspaperAdvertisementManagementSystem.Models;
using NewspaperAdvertisementManagementSystem.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementRepository(ApplicationDbContext context)
        {
            this._context = context;

        }
        public async Task<List<Advertisement>> GetAdvertisements()
        {
            List<Advertisement> advertisementsList;

            advertisementsList = await _context.Advertisements.Include(i => i.Client).ToListAsync();

            return advertisementsList;

        }

        public async Task<int> AddAdvertisement(Advertisement advertisement)
        {
            var result = await _context.Advertisements.AddAsync(advertisement);

            await _context.SaveChangesAsync();

            return result.Entity.AdvertisementId;
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

        public async Task DeleteAdvertisement(int AdvertisementId)
        {
            var result = await _context.Advertisements.FirstOrDefaultAsync(x => x.AdvertisementId == AdvertisementId);

            _context.Advertisements.Remove(result);

            await _context.SaveChangesAsync();

        }

    }
}