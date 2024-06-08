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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> AddBookAsync(BookDto book)
        {
           return await _bookRepository.AddBook(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
           return await _bookRepository.DeleteBook(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(int? pageNumber)
        {
            return await _bookRepository.GetAllBooks(pageNumber);   
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _bookRepository.Getbook(id);
        }

        public async Task<IEnumerable<Book>> SearchAsync(string searchQuery, int? pageNumber)
        {
            return await _bookRepository.Search(searchQuery, pageNumber);
        }

        public async Task<bool> UpdateBookDetailsAsync(int id, BookDto book)
        {
            return await _bookRepository.UpdateBookDetails(id, book);
        }
    }
}
