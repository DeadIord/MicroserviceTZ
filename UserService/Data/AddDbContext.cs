using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace UserService.Data
{
    public class AddDbContext : DbContext
    {
        public AddDbContext(DbContextOptions<AddDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
         
            

            var users = new List<User>
            {
                new User { Id = 1, Username = "Пользователь1", PasswordHash = HashPassword("test1") },
                new User { Id = 2, Username = "Пользователь2", PasswordHash = HashPassword("test2") },
                new User { Id = 3, Username = "zz", PasswordHash = HashPassword("test3") }

            };

            modelBuilder.Entity<User>().HasData(users);

           
            var orders = new List<Order>
            {
                new Order { Id = 1, UserId = 1, OrderDate = DateTime.Now.AddDays(-2), TotalCost = 134000 },
                new Order { Id = 2, UserId = 1, OrderDate = DateTime.Now.AddDays(-1), TotalCost = 130000 },
                new Order { Id = 3, UserId = 2, OrderDate = DateTime.Now, TotalCost = 105000 }
            };


            modelBuilder.Entity<Order>().HasData(orders);

            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = 1, ProductId = 1, Quantity = 2, OrderId = 1 },
                new OrderItem { Id = 2, ProductId = 2, Quantity = 1, OrderId = 1 },
                new OrderItem { Id = 3, ProductId = 4, Quantity = 1, OrderId = 2 },
                new OrderItem { Id = 4, ProductId = 5, Quantity = 1, OrderId = 2 },
                new OrderItem { Id = 5, ProductId = 3, Quantity = 3, OrderId = 3 },
                new OrderItem { Id = 6, ProductId = 6, Quantity = 1, OrderId = 3 },
                new OrderItem { Id = 7, ProductId = 7, Quantity = 2, OrderId = 3 }
            };

            modelBuilder.Entity<OrderItem>().HasData(orderItems);



        }
        private string HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
