using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Data;
using OrderService.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchService.Core.Commands;

namespace ProductService.Rabbit
{

    public class SearchRequestHandler : IConsumer<SearchRequest>
    {
        private readonly AddProductDbContext _dbContext;
        private readonly ILogger<SearchRequestHandler> _logger;

        public SearchRequestHandler(AddProductDbContext dbContext, ILogger<SearchRequestHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SearchRequest> context)
        {
            var searchRequest = context.Message;

            _logger.LogInformation("Получен запрос на поиск: {Text}", searchRequest.Text);

            var searchResult = await PerformSearch(searchRequest.Text);

            _logger.LogInformation("Поиск завершен. Найдено {Count} результатов", searchResult.Count);

            var searchResponse = new SearchResponse { Data = searchResult };

            await context.RespondAsync(searchResponse);
        }

        private async Task<List<object>> PerformSearch(string searchText)
        {
            var users = await _dbContext.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{searchText}%"))
                .Select(p => new { p.Name })
                .ToListAsync();

            var results = users.Cast<object>().ToList();
            return results;
        }

    }
}