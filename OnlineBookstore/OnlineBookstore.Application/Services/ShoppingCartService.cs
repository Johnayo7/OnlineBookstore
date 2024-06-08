using OnlineBookstore.Application.IServices;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.Interfaces;
using OnlineBookstore.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<bool> AddToCartAsync(CartItemDto cartItem)
        {
           return await _shoppingCartRepository.AddToCart(cartItem);
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetCartItemsAsync(int? pageNumber)
        {
            return await _shoppingCartRepository.GetCartItems(pageNumber);
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            return await _shoppingCartRepository.RemoveFromCart(id);
        }
    }
}
