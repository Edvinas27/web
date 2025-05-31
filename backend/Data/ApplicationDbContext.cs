using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Cart;
using backend.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>()
            .HasMany(c => c.CartItems)
            .WithOne(c => c.Cart)
            .HasForeignKey(c => c.CartId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
            .HasOne(c => c.Product)
            .WithMany()
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
            // dont allow duplicate combinations of cartId + productId
            .HasIndex(c => new { c.CartId, c.ProductId })
            .IsUnique();

            modelBuilder.Entity<Cart>()
                .HasIndex(c => c.GuestId)
                .IsUnique();
                
        }
    }
}