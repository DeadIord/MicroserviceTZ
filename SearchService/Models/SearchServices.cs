using ProductService.Models;
using SearchService.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace SearchService.Models
{
    public class SearchServices
    {
        private readonly UserServices _userServices;
        private readonly ProductServices _productServices;

        public SearchServices(UserServices userServices, ProductServices productServices)
        {
            _userServices = userServices;
            _productServices = productServices;
        }

        public async  Task<List<object>> SearchAsync(string text)
        {
            var users = (await _userServices.GetAllUserAsync())
                 .Where(u => u.Username.ToLower().Contains(text))
                 .ToList();

            var products = (await _productServices.GetAllProductsAsync())
                .Where(p => p.Name.ToLower().Contains(text))
                .ToList();

            var searchResults = new List<object>();
            searchResults.AddRange(users);
            searchResults.AddRange(products);

            return searchResults;
        }
    }
}
