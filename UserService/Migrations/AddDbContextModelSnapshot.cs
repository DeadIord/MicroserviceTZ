﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Data;

namespace UserService.Migrations
{
    [DbContext(typeof(AddDbContext))]
    partial class AddDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserService.Data.Order", b =>
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
                            OrderDate = new DateTime(2023, 8, 11, 17, 20, 48, 493, DateTimeKind.Local).AddTicks(1232),
                            TotalCost = 134000.0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            OrderDate = new DateTime(2023, 8, 12, 17, 20, 48, 494, DateTimeKind.Local).AddTicks(4299),
                            TotalCost = 130000.0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            OrderDate = new DateTime(2023, 8, 13, 17, 20, 48, 494, DateTimeKind.Local).AddTicks(4324),
                            TotalCost = 105000.0,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("UserService.Data.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 2,
                            OrderId = 1,
                            ProductId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 3,
                            OrderId = 2,
                            ProductId = 4,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 4,
                            OrderId = 2,
                            ProductId = 5,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 5,
                            OrderId = 3,
                            ProductId = 3,
                            Quantity = 3
                        },
                        new
                        {
                            Id = 6,
                            OrderId = 3,
                            ProductId = 6,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 7,
                            OrderId = 3,
                            ProductId = 7,
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("UserService.Data.User", b =>
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
                            PasswordHash = "eMQ9SvyCXY50iBSyLEenyU3CW4JGynUetmYmelYlugwXvhZyU7PDDV36O5SCf73ntU1yBwVhn5LgfEnmcGtQkg==",
                            Username = "Пользователь1"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = "A8FiqLytmOs33ndcxQEwDzACXkpyTs+PeCQ73rokYRfk+jiDeyjsaaZLxWk2fKtSFxAHueO4wh+I/QcmdiXiPw==",
                            Username = "Пользователь2"
                        },
                        new
                        {
                            Id = 3,
                            PasswordHash = "mcnEogeNwj+oH3b9LeBmSlCwv3lp6ovJ3VTaBXJPfgGbj+iDPwIY9GP61/OwwJWPAy6qGvfUD35AMR5VHbEwrg==",
                            Username = "zz"
                        });
                });

            modelBuilder.Entity("UserService.Data.Order", b =>
                {
                    b.HasOne("UserService.Data.User", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserService.Data.OrderItem", b =>
                {
                    b.HasOne("UserService.Data.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("UserService.Data.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("UserService.Data.User", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
