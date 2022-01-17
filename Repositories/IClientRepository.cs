using System;
using System.Threading.Tasks;
using NewspaperAdvertisementManagementSystem.Models;
namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetClientById(string ClientId);

        // Task<int> AddClient(Client client);

        Task<Client> UpdateClient(Client client);

        Task<ClientInfo> GetClientInfo();
    }
}