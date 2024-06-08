using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.API.DTOs;
using OnlineBookstore.Application.IServices;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.LoggerManager;
using OnlineBookstore.Domain.ViewModels;

namespace OnlineBookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILoggerManager _logger;

        public BookController(IBookService bookService, ILoggerManager logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks(int? pageNumber = 1)
        {
            try
            {
                if (!pageNumber.HasValue || pageNumber.Value < 1)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid Page number", null));
                }

                var allBooks = await _bookService.GetAllBooksAsync(pageNumber);

                return Ok(ApiResponse<object>.Response(true, "All Books retrieved successfully", allBooks));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

        [HttpGet("GetBook/{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                if ( id < 1)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid, input must be greater than 0.", null));
                }

                var book = await _bookService.GetBookAsync(id);

                return Ok(ApiResponse<object>.Response(true, "All Books retrieved successfully", book));


            }
            catch(Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook (BookDto book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid input, try again", null));
                }

                var addBook = await _bookService.AddBookAsync(book);

                return Ok(ApiResponse<object>.Response(true, "Book added successfully", null));
            }
            catch(Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

        [HttpPut("UpdateBookDetails/{id}")]
        public async Task<IActionResult> UpdateBookDetails(int id, BookDto book)
        {
            try
            {
                if (ModelState.IsValid || id < 1)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid input, try again", null));
                }

                var updateBook = await _bookService.UpdateBookDetailsAsync(id, book);

                return Ok(ApiResponse<object>.Response(true, "Book details updated successfully", null));
            }
            catch(Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string searchQuery, int? pageNumber = 1)
        {
            try
            {
                if (!pageNumber.HasValue || pageNumber.Value < 1 || string.IsNullOrWhiteSpace(searchQuery))
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid input, try again", null));
                }

                var searchResults = await _bookService.SearchAsync(searchQuery, pageNumber);

                return Ok(ApiResponse<object>.Response(true, "Search result retrieved successfully", searchResults));

            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid input, try again", null));
                }

                var book = await _bookService.DeleteBookAsync(id);

                return Ok(ApiResponse<object>.Response(true, "Book deleted successfully", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }

        }
    }
}
