using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineBookstore.API.Controllers;
using OnlineBookstore.API.DTOs;
using OnlineBookstore.Application.IServices;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.LoggerManager;
using OnlineBookstore.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Test
{
    public class ShoppingCartControllerTests
    {

        private readonly ShoppingCartController _controller;
        private readonly Mock<IShoppingCartService> _mockShoppingCartService;
        private readonly Mock<ILoggerManager> _mockLogger;

        public ShoppingCartControllerTests()
        {
            _mockShoppingCartService = new Mock<IShoppingCartService>();
            _mockLogger = new Mock<ILoggerManager>();
            _controller = new ShoppingCartController(_mockShoppingCartService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task AddToCart_ReturnsOkResult()
        {
            // Arrange
            var cartItemDto = new CartItemDto { BookId = 1};
            _mockShoppingCartService.Setup(service => service.AddToCartAsync(cartItemDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddToCart(cartItemDto);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("Item added to Cart successfully", apiResponse.Message);
        }

        [Fact]
        public async Task GetCartItems_ReturnsOkResultWithCartItems()
        {
            // Arrange
            var cartItems = new List<ShoppingCartItem>
        {
            new ShoppingCartItem { Id = 1, BookId = 1 },
            new ShoppingCartItem { Id = 2, BookId = 2 }
        };
            _mockShoppingCartService.Setup(service => service.GetCartItemsAsync(It.IsAny<int?>())).ReturnsAsync(cartItems);

            // Act
            var result = await _controller.GetCartItems();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("Shopping Cart items retrieved successfully", apiResponse.Message);
            var returnValue = Assert.IsAssignableFrom<List<ShoppingCartItem>>(apiResponse.Data);
            Assert.Equal(cartItems.Count, returnValue.Count);
        }

        [Fact]
        public async Task RemoveFromCart_ReturnsOkResult()
        {
            // Arrange
            int cartItemId = 1;
            _mockShoppingCartService.Setup(service => service.RemoveFromCartAsync(cartItemId)).ReturnsAsync(true);

            // Act
            var result = await _controller.RemoveFromCart(cartItemId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<object>>(actionResult.Value);
            Assert.True(apiResponse.Status);
            Assert.Equal("Item removed successfully", apiResponse.Message);
        }
    }
}
