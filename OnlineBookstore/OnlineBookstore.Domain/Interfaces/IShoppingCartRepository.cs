using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCartItem>> GetCartItems(int? pageNumber);
        Task<bool> AddToCart(CartItemDto cartItem);
        Task<bool> RemoveFromCart(int id);
    }
}
