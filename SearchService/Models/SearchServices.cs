using Microsoft.Extensions.Logging;
using MassTransit;
using SearchService.Rabbit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchService
{
    public class SearchServices
    {
        private readonly IRequestClient<SearchRequest> _requestClient;
        private readonly ILogger<SearchServices> _logger;

        public SearchServices(IRequestClient<SearchRequest> requestClient, ILogger<SearchServices> logger)
        {
            _requestClient = requestClient;
            _logger = logger;
        }
        public async Task<List<object>> SearchAsync(string text)
        {
            _logger.LogInformation("Отправка запроса на поиск");
            var searchRequest = new SearchRequest
            {
                Text = text
            };

            var response = await _requestClient.GetResponse<SearchResponse>(searchRequest);
            var searchResponse = response.Message;
            _logger.LogInformation("Получен ответ на запрос с данными");
            return searchResponse.Data;
        }

    }
}