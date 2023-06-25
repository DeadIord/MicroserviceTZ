using MicroserviceTz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace MicroserviceTz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }
       
        [HttpGet("search")]
      
        public IActionResult Search(string Text)
        {
            var searchResults = _searchService.Search(Text);
            if (searchResults == null)
                return BadRequest(new { message = "Данный пользователь/пользователь отсутствует" });
            return Ok(searchResults);
        }
    }
}
