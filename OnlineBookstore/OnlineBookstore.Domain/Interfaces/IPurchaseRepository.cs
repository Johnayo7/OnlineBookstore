using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<bool> Checkout(PaymentMethod paymentMethod = PaymentMethod.Web);
        Task<IEnumerable<PurchaseHistory>> GetPurchaseHistory(int? pageNumber);
    }
}
