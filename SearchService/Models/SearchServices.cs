using ProductsService.Models;
using SearchService.Data;
using System.Collections.Generic;
using System.Linq;
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

        public List<object> Search(string text)
        {
            var users = _userServices.GetAllUser()
                .Where(u => u.Username.ToLower().Contains(text))
                .ToList();

            var products = _productServices.GetAllProducts()
                .Where(p => p.Name.ToLower().Contains(text))
                .ToList();

            var searchResults = new List<object>();
            searchResults.AddRange(users);
            searchResults.AddRange(products);

            return searchResults;
        }
    }
}
