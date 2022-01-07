using System.Threading.Tasks;
using NewspaperAdvertisementManagementSystem.Contexts;
using NewspaperAdvertisementManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<Client> GetClientById(int ClientId)
        {

            Client client = await _context.Clients.FirstOrDefaultAsync(client => client.ClientId == ClientId);

            return client;

        }
    }
}