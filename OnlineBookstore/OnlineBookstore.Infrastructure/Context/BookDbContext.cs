using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Infrastructure.Context
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) 
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public DbSet<PurchaseHistoryItem> PurchaseHistoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PurchaseHistory>()
                .Property(ph => ph.PaymentMethod)
                .HasConversion<string>();

            modelBuilder.Entity<PurchaseHistoryItem>()
                .HasOne(p => p.PurchaseHistory)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.PurchaseHistoryId);

            modelBuilder.Entity<PurchaseHistoryItem>()
                .HasOne(p => p.Book)
                .WithMany()
                .HasForeignKey(p => p.BookId);
        }
    }
}
