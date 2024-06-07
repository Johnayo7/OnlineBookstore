using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Entities
{
    public class ShoppingCartItem : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
