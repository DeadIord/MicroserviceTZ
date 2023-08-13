using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace ProductService.Data
{
    public class AddProductDbContext  : DbContext
    {
        public AddProductDbContext (DbContextOptions<AddProductDbContext > options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Ноутбук", Price = 52000 },
                new Product { Id = 2, Name = "Смартфон", Price = 30000 },
                new Product { Id = 3, Name = "Планшет", Price = 25000 },
                new Product { Id = 4, Name = "Компьютер", Price = 70000 },
                new Product { Id = 5, Name = "Телевизор", Price = 60000 },
                new Product { Id = 6, Name = "Фотоаппарат", Price = 40000 },
                new Product { Id = 7, Name = "Наушники", Price = 5000 }
            };

            modelBuilder.Entity<Product>().HasData(products);

            


        }

    }
}
