using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineBookstore.API.Controllers;
using OnlineBookstore.API.DTOs;
using OnlineBookstore.Application.IServices;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.LoggerManager;
using OnlineBookstore.Domain.ViewModels;
using OnlineBookstore.Infrastructure.Context;
using X.PagedList;

namespace OnlineBookstore.Test
{
    public class BookControllerTests
    {
        private readonly BookController _controller;
        private readonly Mock<IBookService> _mockBookService;
        private readonly Mock<ILoggerManager> _mockLogger;

        public BookControllerTests()
        {
            _mockBookService = new Mock<IBookService>();
            _mockLogger = new Mock<ILoggerManager>();
            _controller = new BookController(_mockBookService.Object, _mockLogger.Object);
        }



        [Fact]
        public async Task AddBook_ReturnsOkResult()
        {
            // Arrange
            var bookDto = new BookDto
            {
                Title = "Test Book",
                Genre = "Fiction",
                ISBN = "123456789",
                Author = "Author Name",
                PublicationYear = 2021
            };
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Genre = "Fiction",
                ISBN = "123456789",
                Author = "Author Name",
                PublicationYear = 2021,
                DateCreated = DateTime.UtcNow
            };

            _mockBookService.Setup(service => service.AddBookAsync(bookDto));

            // Act
            var result = await _controller.AddBook(bookDto);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("Book added successfully", apiResponse.Message);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOkResultWithBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ISBN = "123", Author = "Author 1", PublicationYear = 2021 },
                new Book { Id = 2, Title = "Book 2", Genre = "Non-Fiction", ISBN = "456", Author = "Author 2", PublicationYear = 2020 }
            };
            var pagedBooks = new StaticPagedList<Book>(books, 1, 10, books.Count);

            _mockBookService.Setup(service => service.GetAllBooksAsync(1)).ReturnsAsync(pagedBooks);

            // Act
            var result = await _controller.GetAllBooks(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("All Books retrieved successfully", apiResponse.Message);
        }

        [Fact]
        public async Task GetBook_ReturnsOkResultWithBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ISBN = "123", Author = "Author 1", PublicationYear = 2021 };
            _mockBookService.Setup(service => service.GetBookAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _controller.GetBook(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("All Books retrieved successfully", apiResponse.Message);
        }

        [Fact]
        public async Task UpdateBookDetails_ReturnsOkResult()
        {
            // Arrange
            var bookDto = new BookDto
            {
                Title = "Updated Book",
                Genre = "Fiction",
                ISBN = "987654321",
                Author = "Updated Author",
                PublicationYear = 2022
            };

            _mockBookService.Setup(service => service.UpdateBookDetailsAsync(1, bookDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateBookDetails(1, bookDto);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("Book details updated successfully", apiResponse.Message);
        }

        [Fact]
        public async Task Search_ReturnsOkResultWithSearchResults()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Search Book 1", Genre = "Fiction", ISBN = "123", Author = "Author 1", PublicationYear = 2021 },
                new Book { Id = 2, Title = "Search Book 2", Genre = "Non-Fiction", ISBN = "456", Author = "Author 2", PublicationYear = 2020 }
            };
            var pagedBooks = new StaticPagedList<Book>(books, 1, 10, books.Count);

            _mockBookService.Setup(service => service.SearchAsync("Search", 1)).ReturnsAsync(pagedBooks);

            // Act
            var result = await _controller.Search("Search", 1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("Search result retrieved successfully", apiResponse.Message);
        }
        [Fact]
        public async Task DeleteBook_ReturnsOkResult()
        {
            // Arrange
            _mockBookService.Setup(service => service.DeleteBookAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteBook(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("Book deleted successfully", apiResponse.Message);
        }
    }
}