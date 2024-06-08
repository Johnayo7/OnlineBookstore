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
    public class ShoppingRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;

        public ShoppingRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooks(int? pageNumber)
        {
            int page = pageNumber ?? 1;
            int pageSize = 10;

            var allBooksQuery = await _dbContext.Books.ToPagedListAsync(page, pageSize);
            return allBooksQuery;   
        }

        public async Task<Book> Getbook(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var bookExists = await _dbContext.Books.FindAsync(id);

            if (bookExists is null)
            {
                return null;
            }

            return bookExists;
        }

        public async Task<IEnumerable<Book>> Search(string searchQuery, int? pageNumber)
        {
            int page = pageNumber ?? 1;
            int pageSize = 10;

            if (searchQuery == null || string.IsNullOrWhiteSpace(searchQuery))
            {
                return null;
            }
            
            searchQuery = $"%{searchQuery.ToLower()}%";    

            var searchResults = await _dbContext.Books.Where(s =>
                          EF.Functions.Like(s.Title, searchQuery) ||
                          EF.Functions.Like(s.Genre, searchQuery) ||
                          EF.Functions.Like(s.Author, searchQuery) ||
                          EF.Functions.Like(s.PublicationYear.ToString(), searchQuery)).ToPagedListAsync(page, pageSize);

            return searchResults;
        }

        public async Task<bool> AddBook(BookDto book)
        {
            if (book is null)
            {
                return false;
            }

            var addBook = new Book()
            {
                Title = book.Title,
                Genre = book.Genre,
                ISBN = book.ISBN,
                Author = book.Author,
                PublicationYear = book.PublicationYear
            };

            _dbContext.Books.Add(addBook);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBookDetails(int id, BookDto book)
        {
            var existingBook = await Getbook(id);

            existingBook.Title = book.Title;
            existingBook.Genre = book.Genre;
            existingBook.ISBN = book.ISBN;
            existingBook.Author = book.Author;
            existingBook.PublicationYear = book.PublicationYear;
            existingBook.UpdatedAt = DateTime.UtcNow;

            _dbContext.Books.Add(existingBook);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await Getbook(id);   

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
