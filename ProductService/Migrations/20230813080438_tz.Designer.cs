﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductService.Data;

namespace ProductService.Migrations
{
    [DbContext(typeof(AddProductDbContext))]
    [Migration("20230813080438_tz")]
    partial class tz
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductService.Data.Product", b =>
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
#pragma warning restore 612, 618
        }
    }
}