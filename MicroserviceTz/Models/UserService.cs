using MicroserviceTz.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTz.Models
{
    public class UserService
    {
        private readonly AddDbContext _dbContext;

        public UserService(AddDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Authenticate(string username, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Username == username);

            if (user == null || VerifyPasswordHash(password, user.PasswordHash))
                return null;

            user.Token = GenerateToken();

            _dbContext.SaveChanges();

            return user;
        }


        public IEnumerable<User> GetAllUser()
        {
            return _dbContext.Users.ToList();
        }


        public OrderDetailsVM GetOrderDetails(int orderId)
        {
            var order = _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
           
                return null;
            

            var orderDetails = new OrderDetailsVM
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalCost = order.TotalCost,
                OrderItems = order.OrderItems.Select(oi => new OrderItemVM
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity
                }).ToList()
            };

            return orderDetails;
        }

        public List<OrderHistoryItemVM> GetOrderHistoryForUser(int userId)
        {
            var orderHistory = _dbContext.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderHistoryItemVM
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalCost = o.TotalCost,
                    TotalQuantity = o.OrderItems.Sum(oi => oi.Quantity)
                })
                .ToList();

            return orderHistory;
        }


        private bool VerifyPasswordHash(string password, string storedHash)
        {
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);

            using (var hmac = new HMACSHA512())
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHashBytes[i])
                        return false;
                }

                return true;
            }
        }


        private string GenerateToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];

                rngCryptoServiceProvider.GetBytes(tokenData);

                return Convert.ToBase64String(tokenData);
            }
        }


        public User Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }






        public User GetById(int userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public User Update(int userId, User updatedUser)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
                return null;

        

            user.Username = updatedUser.Username;
            user.PasswordHash = updatedUser.Username;
            _dbContext.SaveChanges();

            return user;
        }

        public User Delete(int userId)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
                return null;

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return user;
        }





    }
}
