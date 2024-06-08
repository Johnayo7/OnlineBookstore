using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.Enum;
using OnlineBookstore.Domain.Interfaces;
using OnlineBookstore.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineBookstore.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly BookDbContext _bookDbContext;

        public PurchaseRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public async Task<bool> Checkout (PaymentMethod paymentMethod = PaymentMethod.Web)
        {
            var cartItems = await _bookDbContext.ShoppingCartItems.Include(i => i.Book).ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return false;
            }

            var purchaseHistory = new PurchaseHistory
            {
                DateCreated = DateTime.UtcNow,           
                Items = cartItems.Select(cartItem => new PurchaseHistoryItem
                {
                    BookId = cartItem.BookId
                }).ToList()
            };

            if (paymentMethod != PaymentMethod.Web || paymentMethod != PaymentMethod.Transfer || paymentMethod != PaymentMethod.USSD)
            {
                return false;
            }

            purchaseHistory.PaymentMethod = paymentMethod;
            purchaseHistory.PurchaseDate = (DateTime)(purchaseHistory.UpdatedAt = DateTime.UtcNow);
     
            foreach (var bookId in purchaseHistory.Items)
            {
                var purchaseHistoryItem = new PurchaseHistoryItem
                {
                    BookId = bookId.BookId,
                    PurchaseHistoryId = purchaseHistory.Id
                };
                _bookDbContext.PurchaseHistoryItems.Add(purchaseHistoryItem);

                #region Skip
                // Remove items from the shopping cart
                /*  var cartItem = await _bookDbContext.ShoppingCartItems.FirstOrDefaultAsync(sci => sci.BookId == bookId.BookId);
                  if (cartItem != null)
                  {
                      _bookDbContext.ShoppingCartItems.Remove(cartItem);
                  }*/
                #endregion
            }

            _bookDbContext.PurchaseHistories.Add(purchaseHistory);

            _bookDbContext.ShoppingCartItems.RemoveRange(cartItems);

            await _bookDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PurchaseHistory>> GetPurchaseHistory(int? pageNumber)
        {
            int page = pageNumber ?? 1;
            int pageSize = 10;

            return await _bookDbContext.PurchaseHistories.Include(ph => ph.Items)
                                                         .ThenInclude(i => i.Book).ToPagedListAsync(page, pageSize);    
        }
    }
}
