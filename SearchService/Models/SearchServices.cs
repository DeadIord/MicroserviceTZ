using MassTransit;
using Microsoft.Extensions.Logging;
using SearchService.Core.Commands;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService
{

    public class SearchServices
    {
        private readonly IRequestClient<SearchRequest> _searchService1RequestClient;
        private readonly IRequestClient<SearchRequest> _searchService2RequestClient; 

        private readonly ILogger<SearchServices> _logger;

        public SearchServices(IRequestClient<SearchRequest> searchService1RequestClient,  IRequestClient<SearchRequest> searchService2RequestClient, ILogger<SearchServices> logger)
        {
            _searchService1RequestClient = searchService1RequestClient;
            _searchService2RequestClient = searchService2RequestClient;

            _logger = logger;
        }

        public async Task<List<object>> SearchAsync(string text)
        {
            var searchRequest = new SearchRequest
            {
                Text = text
            };

            _logger.LogInformation("Отправка запроса на поиск: {Text}", searchRequest.Text);

            var responseTask1 = _searchService1RequestClient.GetResponse<SearchResponse>(searchRequest);
            var responseTask2 = _searchService2RequestClient.GetResponse<SearchResponse>(searchRequest);

            _logger.LogInformation("Получен ответ на запрос поиска");

            await Task.WhenAll(responseTask1, responseTask2);

            var searchResponse1 = responseTask1.Result.Message;
            var searchResponse2 = responseTask2.Result.Message;
            var combinedData = new List<object>();
            combinedData.AddRange(searchResponse1.Data);
            combinedData.AddRange(searchResponse2.Data);

            return combinedData;
        }

    }
}
