using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.IServices
{
    public interface IPurchaseService
    {
        Task<bool> CheckoutAsync(PaymentMethod paymentMethod = PaymentMethod.Web);
        Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryAsync(int? pageNumber);
    }
}
