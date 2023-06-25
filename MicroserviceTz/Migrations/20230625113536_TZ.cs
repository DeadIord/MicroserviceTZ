﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroserviceTz.Migrations
{
    public partial class TZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Ноутбук", 50000.0 },
                    { 2, "Смартфон", 30000.0 },
                    { 3, "Планшет", 25000.0 },
                    { 4, "Компьютер", 70000.0 },
                    { 5, "Телевизор", 60000.0 },
                    { 6, "Фотоаппарат", 40000.0 },
                    { 7, "Наушники", 5000.0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Token", "Username" },
                values: new object[,]
                {
                    { 1, "WzTnTi7mAi5n+g3u0aZgPjw2CxAXW7HQdGNU9vgRwqObS7Az6d0SVXnTBaMvuW5TifARaTvyGVuiDIsg3cYZGQ==", null, "Пользователь1" },
                    { 2, "dSvXsqoyu815wjm3/o8xSXYbfxENwzKtnMqLbB2ZNE15+w6XNi5K465DeJecIzuCSUoUtxLd3CnoHZUjNlfadw==", null, "Пользователь2" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "TotalCost", "UserId" },
                values: new object[] { 1, new DateTime(2023, 6, 23, 15, 35, 35, 628, DateTimeKind.Local).AddTicks(2965), 130000.0, 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "TotalCost", "UserId" },
                values: new object[] { 2, new DateTime(2023, 6, 24, 15, 35, 35, 629, DateTimeKind.Local).AddTicks(4931), 130000.0, 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "TotalCost", "UserId" },
                values: new object[] { 3, new DateTime(2023, 6, 25, 15, 35, 35, 629, DateTimeKind.Local).AddTicks(4949), 105000.0, 2 });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "OrdersId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 1, 2, 1 },
                    { 3, 2, 4, 1 },
                    { 4, 2, 5, 1 },
                    { 5, 3, 3, 3 },
                    { 6, 3, 6, 1 },
                    { 7, 3, 7, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrdersId",
                table: "OrderItem",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
