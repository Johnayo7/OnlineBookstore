using OnlineBookstore.Application.IServices;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.Enum;
using OnlineBookstore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<bool> CheckoutAsync(PaymentMethod paymentMethod)
        {
            return await _purchaseRepository.Checkout(paymentMethod);
        }

        public async Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryAsync(int? pageNumber)
        {
           return await _purchaseRepository.GetPurchaseHistory(pageNumber);
        }
    }
}
