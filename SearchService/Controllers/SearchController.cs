using SearchService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using ProductsService.Models;
using UserService.Models;

namespace SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchServices _searchService;
       

        public SearchController(SearchServices searchService)
        {
            _searchService = searchService;
          
        }

        [HttpGet("search")]
        public IActionResult Search(string text)
        {
            var searchResults = _searchService.Search(text);
            if (searchResults.Count == 0)
                return BadRequest(new { message = "Пользователи или товары не найдены" });
            return Ok(searchResults);
        }

    }
}
