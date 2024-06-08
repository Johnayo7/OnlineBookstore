using OnlineBookstore.Application.IServices;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Interfaces;
using OnlineBookstore.Domain.LoggerManager;
using OnlineBookstore.Infrastructure.Repositories;

namespace OnlineBookstore.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static  IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBookRepository, ShoppingRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ILoggerManager, LoggerManager>();
            return services;
        }
    }
}
