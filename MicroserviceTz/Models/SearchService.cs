using MicroserviceTz.Data;
using System.Collections.Generic;
using System.Linq;

namespace MicroserviceTz.Models
{
    public class SearchService
    {
        private readonly AddDbContext _dbContext;

        public SearchService(AddDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<object> Search(string Text)
        {
            var users = _dbContext.Users
                .Where(u => u.Username.Contains(Text))
                .ToList();

            var products = _dbContext.Products
                .Where(p => p.Name.Contains(Text))
                .ToList();

            var searchResults = new List<object>();
            searchResults.AddRange(users);
            searchResults.AddRange(products);

            return searchResults;
        }

    }
}
