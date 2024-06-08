using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks(int? pageNumber);
        Task<Book> Getbook(int id);
        Task<IEnumerable<Book>> Search(string searchQuery, int? pageNumber);
        Task<bool> AddBook(BookDto book);
        Task<bool> UpdateBookDetails(int id, BookDto book);
        Task<bool> DeleteBook(int id);

    }
}
