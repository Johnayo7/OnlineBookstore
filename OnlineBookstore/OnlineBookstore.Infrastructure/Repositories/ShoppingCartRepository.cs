using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.Interfaces;
using OnlineBookstore.Domain.ViewModels;
using OnlineBookstore.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineBookstore.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly BookDbContext _bookDbContext;

        public ShoppingCartRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetCartItems (int? pageNumber)
        {
            int page = pageNumber ?? 1;
            int pageSize = 10;

            var cartItems = await _bookDbContext.ShoppingCartItems
                                                .Include(i => i.Book).ToPagedListAsync(page, pageSize);

            return cartItems;
        }

        public async Task<bool> AddToCart(CartItemDto cartItem)
        {
            if (cartItem == null)
            {
                return false;
            }

            var newItem = new ShoppingCartItem()
            {
                BookId = cartItem.BookId,
                Book =  cartItem.Book
            };

            _bookDbContext.ShoppingCartItems.Add(newItem);
            await _bookDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFromCart(int id)
        {
            var item = await GetShoppingCartItemById(id);

            _bookDbContext.ShoppingCartItems.Remove(item);
            await _bookDbContext.SaveChangesAsync();

            return true;
        }


        private async Task<ShoppingCartItem> GetShoppingCartItemById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var item = await _bookDbContext.ShoppingCartItems.FindAsync(id);

            if (item == null)
            {
                return null;
            }

            return item;
        }
    }
}
