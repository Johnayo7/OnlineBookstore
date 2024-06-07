using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Entities
{
    public class PurchaseHistoryItem : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public int PurchaseHistoryId { get; set; }
        public PurchaseHistory PurchaseHistory { get; set; } = null!;
    }
}
