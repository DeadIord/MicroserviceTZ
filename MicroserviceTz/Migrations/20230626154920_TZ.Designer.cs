﻿// <auto-generated />
using System;
using MicroserviceTz.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MicroserviceTz.Migrations
{
    [DbContext(typeof(AddDbContext))]
    [Migration("20230626154920_TZ")]
    partial class TZ
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MicroserviceTz.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalCost")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderDate = new DateTime(2023, 6, 24, 19, 49, 19, 954, DateTimeKind.Local).AddTicks(3813),
                            TotalCost = 134000.0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            OrderDate = new DateTime(2023, 6, 25, 19, 49, 19, 955, DateTimeKind.Local).AddTicks(6211),
                            TotalCost = 130000.0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            OrderDate = new DateTime(2023, 6, 26, 19, 49, 19, 955, DateTimeKind.Local).AddTicks(6236),
                            TotalCost = 105000.0,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("MicroserviceTz.Models.OrderItem", b =>
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

                    b.HasIndex("OrdersId");

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

            modelBuilder.Entity("MicroserviceTz.Models.Product", b =>
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

            modelBuilder.Entity("MicroserviceTz.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "pouaGClpnZzjfrgV+vFycNqRkzb/56qZTnn73sAcC5TSsILPWwPuBuLDAY3w99SXU3o/mr6quv7JK5lERMAfHA==",
                            Username = "Пользователь1"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = "wms55DcB/YEKa3q88/ZO7FFQybMO6nx0lN34HGJ+tzP7KhEc7QtUCBjAopFwwFU2UGYHiTM5HbwRmAIiQ/FJVA==",
                            Username = "Пользователь2"
                        });
                });

            modelBuilder.Entity("MicroserviceTz.Models.Order", b =>
                {
                    b.HasOne("MicroserviceTz.Models.User", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MicroserviceTz.Models.OrderItem", b =>
                {
                    b.HasOne("MicroserviceTz.Models.Order", "Orders")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MicroserviceTz.Models.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MicroserviceTz.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("MicroserviceTz.Models.Product", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("MicroserviceTz.Models.User", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
