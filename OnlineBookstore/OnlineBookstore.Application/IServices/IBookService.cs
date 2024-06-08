using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(int? pageNumber);
        Task<Book> GetBookAsync(int id);
        Task<IEnumerable<Book>> SearchAsync(string searchQuery, int? pageNumber);
        Task<bool> AddBookAsync(BookDto book);
        Task<bool> UpdateBookDetailsAsync(int id, BookDto book);
        Task<bool> DeleteBookAsync(int id);

    }
}
