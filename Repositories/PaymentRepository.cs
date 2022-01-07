using System.Threading.Tasks;
using NewspaperAdvertisementManagementSystem.Contexts;
using NewspaperAdvertisementManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace NewspaperAdvertisementManagementSystem.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<int> AddPayment(Payment paymentModel)
        {
            var result = await _context.Payments.AddAsync(paymentModel);
            await _context.SaveChangesAsync();

            return result.Entity.AdvertisementId;
        }

        public async Task<Payment> GetPaymentByAdvertisementId(int AdvertisementId)
        {
            Payment payment = await _context.Payments.FirstOrDefaultAsync(payment => payment.AdvertisementId == AdvertisementId);

            return payment;
        }


    }
}