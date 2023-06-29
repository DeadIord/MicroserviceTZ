using SearchService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult>SearchAsynch(string text)
        {
            var searchResults = await _searchService.SearchAsync(text);
            if (searchResults.Count == 0)
                return BadRequest(new { message = "Пользователи или товары не найдены" });
            return Ok(searchResults);
        }

    }
}
