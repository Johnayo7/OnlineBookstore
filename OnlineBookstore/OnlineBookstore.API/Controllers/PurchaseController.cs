using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.API.DTOs;
using OnlineBookstore.Application.IServices;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Enum;
using OnlineBookstore.Domain.LoggerManager;

namespace OnlineBookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ILoggerManager _logger;
        public PurchaseController(IPurchaseService purchaseService, ILoggerManager logger)
        {
            _purchaseService = purchaseService;
            _logger = logger;
        }


        [HttpGet("GetPurchaseHistory")]

        public async Task<IActionResult> GetPurchaseHistory(int? pageNumber = 1)
        {
            try
            {
                if (!pageNumber.HasValue || pageNumber.Value < 1)
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid Page number", null));
                }

                var purchases = await _purchaseService.GetPurchaseHistoryAsync(pageNumber);

                return Ok(ApiResponse<object>.Response(true, "Purchase history retrieved successfully", purchases));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }

        [HttpPost("CheckOut")]
        public async Task<IActionResult> CheckOut(PaymentMethod paymentMethod = PaymentMethod.Web)
        {
            try
            {
                if (!Enum.IsDefined(typeof(PaymentMethod), paymentMethod))
                {
                    return BadRequest(ApiResponse<object>.Response(false, "Invalid payment method", null));
                }

                var success = await _purchaseService.CheckoutAsync(paymentMethod);

                return Ok(ApiResponse<object>.Response(true, "Payment successful", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred!", ex);
                return BadRequest(ApiResponse<object>.Response(false, $"An error occurred while processing your request {ex.Message}", null));
            }
        }
    }
}
