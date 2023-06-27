using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;
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
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
         
            

            var users = new List<User>
            {
                new User { Id = 1, Username = "Пользователь1", PasswordHash = HashPassword("test1") },
                new User { Id = 2, Username = "Пользователь2", PasswordHash = HashPassword("test2") }
            };

            modelBuilder.Entity<User>().HasData(users);

           
            var orders = new List<Order>
            {
                new Order { Id = 1, UserId = 1, OrderDate = DateTime.Now.AddDays(-2), TotalCost = 134000 },
                new Order { Id = 2, UserId = 1, OrderDate = DateTime.Now.AddDays(-1), TotalCost = 130000 },
                new Order { Id = 3, UserId = 2, OrderDate = DateTime.Now, TotalCost = 105000 }
            };

            modelBuilder.Entity<Order>().HasData(orders);
           


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
