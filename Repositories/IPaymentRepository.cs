using System.Threading.Tasks;
using NewspaperAdvertisementManagementSystem.Models;
namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByAdvertisementId(int AdvertisementId);

        Task<int> AddPayment(Payment payment);
    }
}