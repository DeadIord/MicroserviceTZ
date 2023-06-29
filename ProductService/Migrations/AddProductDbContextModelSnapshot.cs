﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductService.Data;

namespace ProductService.Migrations
{
    [DbContext(typeof(AddProductDbContext))]
    partial class AddProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductService.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrdersId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrdersId = 1,
                            ProductId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 2,
                            OrdersId = 1,
                            ProductId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 3,
                            OrdersId = 2,
                            ProductId = 4,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 4,
                            OrdersId = 2,
                            ProductId = 5,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 5,
                            OrdersId = 3,
                            ProductId = 3,
                            Quantity = 3
                        },
                        new
                        {
                            Id = 6,
                            OrdersId = 3,
                            ProductId = 6,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 7,
                            OrdersId = 3,
                            ProductId = 7,
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("ProductService.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ноутбук",
                            Price = 52000.0
                        },
                        new
                        {
                            Id = 2,
                            Name = "Смартфон",
                            Price = 30000.0
                        },
                        new
                        {
                            Id = 3,
                            Name = "Планшет",
                            Price = 25000.0
                        },
                        new
                        {
                            Id = 4,
                            Name = "Компьютер",
                            Price = 70000.0
                        },
                        new
                        {
                            Id = 5,
                            Name = "Телевизор",
                            Price = 60000.0
                        },
                        new
                        {
                            Id = 6,
                            Name = "Фотоаппарат",
                            Price = 40000.0
                        },
                        new
                        {
                            Id = 7,
                            Name = "Наушники",
                            Price = 5000.0
                        });
                });

            modelBuilder.Entity("ProductService.Models.OrderItem", b =>
                {
                    b.HasOne("ProductService.Models.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductService.Models.Product", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
