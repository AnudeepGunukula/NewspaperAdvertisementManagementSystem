using NewspaperAdvertisementManagementSystem.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public interface IAdvertisementRepository
    {
        Task<List<Advertisement>> GetAdvertisements();

        Task<List<Advertisement>> GetExpiredAdvertisements();

        Task<int> AddAdvertisement(Advertisement advertisement);

        Task<Advertisement> UpdateAdvertisementStatus(int AdvertisementId);

        Task<List<Advertisement>> GetAdvertisementsByClientId(int ClientId);

        // Task<Advertisement> DeleteAdvertisement(int AdvertisementId);



    }
}