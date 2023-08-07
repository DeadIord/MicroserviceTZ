using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchService;
using System;
using System.Threading.Tasks;

namespace SearchService.Controllers
{
    public class SearchController : ControllerBase
    {
        private readonly SearchServices _searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(SearchServices searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync(string text)
        {
            _logger.LogInformation("Получен запрос на поиск");
            var searchResults = await _searchService.SearchAsync(text);

            if (searchResults.Count == 0)
            {
                _logger.LogInformation("Результаты поиска не найдены");
                return BadRequest(new { message = "Пользователи или товары не найдены" });
            }

            _logger.LogInformation("Возвращены результаты поиска для текста");
            return Ok(searchResults);
        }
    }
}