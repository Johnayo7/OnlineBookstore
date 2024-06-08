using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.IServices
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartItem>> GetCartItemsAsync(int? pageNumber);
        Task<bool> AddToCartAsync(CartItemDto cartItem);
        Task<bool> RemoveFromCartAsync(int id);
    }
}
