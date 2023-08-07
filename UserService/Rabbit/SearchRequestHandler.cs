using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.Data;
using Microsoft.EntityFrameworkCore;
using MassTransit;

namespace UserService.Rabbit
{
    public class SearchRequestHandler : IConsumer<SearchRequest>
    {
        private readonly AddDbContext _dbContext;
        private readonly ILogger<SearchRequestHandler> _logger;
        public SearchRequestHandler(AddDbContext dbContext, ILogger<SearchRequestHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<SearchRequest> context)
        {
            _logger.LogInformation("Получен поисковый запрос");
            var searchRequest = context.Message;
            var searchResult = await PerformSearch(searchRequest.Text);
            var searchResponse = new SearchResponse { Data = searchResult };
            _logger.LogInformation("Отправка ответа на поиск");
            await context.RespondAsync(searchResponse);
        }
        private async Task<List<object>> PerformSearch(string searchText)
        {
            var users = await _dbContext.Users
            .Where(p => p.Username.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
            var results = users.Select(p => new { p.Username }).Cast<object>().ToList();
            return results;
        }
    }

}