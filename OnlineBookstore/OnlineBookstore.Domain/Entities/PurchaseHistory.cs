using OnlineBookstore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Entities
{
    public class PurchaseHistory : BaseEntity
    {
        public List<PurchaseHistoryItem> Items { get; set; } = new List<PurchaseHistoryItem>();
        public DateTime PurchaseDate { get; set; } = DateTime.MinValue; 
        public PaymentMethod PaymentMethod { get; set; }
    }
}
