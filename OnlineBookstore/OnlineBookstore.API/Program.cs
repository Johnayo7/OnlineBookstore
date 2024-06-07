
using Microsoft.EntityFrameworkCore;
using OnlineBookstore.API.Extensions;
using OnlineBookstore.Infrastructure.Context;

namespace OnlineBookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            builder.Services.AddDbContext<BookDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BookstoreConnectionString")));

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
