using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.API.DTOs;
using OnlineBookstore.Application.IServices;
using OnlineBookstore.Domain.LoggerManager;
using OnlineBookstore.Domain.ViewModels;

namespace OnlineBookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILoggerManager _logger;
        public ShoppingCartController(IShoppingCartService shoppingCartService, ILoggerManager logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        [HttpGet("GetCartItems")]
        public async Task<IActionResult> GetCartItems(int? pageNumber = 1)
        {
            try
            {
                if (!pageNumber.HasValue || pageNumber.Value < 1)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid Page number", null));
                }

                var items = await _shoppingCartService.GetCartItemsAsync(pageNumber);

                return Ok(ApiResponse<object>.Response(true, "Shopping Cart items retrieved successfully", items));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(CartItemDto item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid input, try again", null));
                }

                var addItem = await _shoppingCartService.AddToCartAsync(item);

                return Ok(ApiResponse<object>.Response(true, "Item added to Cart successfully", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

        [HttpDelete("RemoveFromCart/{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid input, try again", null));
                }

                var item = await _shoppingCartService.RemoveFromCartAsync(id);

                return Ok(ApiResponse<object>.Response(true, "Item removed successfully", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

    }
}
