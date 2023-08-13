using MassTransit;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using ProductService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Core.Commands;
using ProductService.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProductService.Rabbit
{

    public class OrderRequestHandler : IConsumer<RequestForOrderHistory>
    {
        private readonly ILogger<OrderRequestHandler> _logger;
        private readonly AddProductDbContext _dbContext;

        public OrderRequestHandler(ILogger<OrderRequestHandler> logger, AddProductDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<RequestForOrderHistory> context)
        {
            var request = context.Message;

            _logger.LogInformation("Получен запрос на получение информации о продуктах по Ids: {ProductIds}", string.Join(", ", request.ProductIds));

            var products = await _dbContext.Products
                .Where(p => request.ProductIds.Contains(p.Id))
                .Select(p => new ProductsInfo
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToListAsync();

            var response = new GetProductsResponse
            {
                Products = products
            };

            _logger.LogInformation("Отправка ответа с информацией о продуктах");

            await context.RespondAsync(response);
        }
    }
}

