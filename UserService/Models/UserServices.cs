using UserService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UserService.ViewModel;
using ProductsService.Models;
using ProductsService.Data;

namespace UserService.Models
{
    public class UserServices
    {
        private readonly AddDbContext _dbContext;
        private readonly AddProductDbContext _productDbContext;


        public UserServices(AddDbContext dbContext, AddProductDbContext productDbContext)
        {

            _productDbContext = productDbContext;
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


        public OrderDetailsVM GetOrderDetails(int orderId, int userId)
        {
            var order = _dbContext.Orders
                .Where(i => i.UserId == userId)
                .FirstOrDefault(o => o.Id == orderId)
               ;

            if (order == null)
                return null;

            var orderItems = _productDbContext.OrderItem
                .Where(oi => oi.OrdersId == orderId)
                .Include(oi => oi.Product)
                .ToList();

            var orderDetails = new OrderDetailsVM
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalCost = order.TotalCost,
                OrderItems = orderItems.Select(oi => new OrderItemVM
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity
                }).ToList()
            };

            return orderDetails;
        }



        public List<OrderHistoryItemVM> GetOrderHistoryForUser(int userId)
        {
            var orders = _dbContext.Orders.Where(o => o.UserId == userId).ToList();

            var orderHistory = orders.Select(o => new OrderHistoryItemVM
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalCost = o.TotalCost,
                TotalQuantity = GetTotalQuantityForOrder(o.Id)
            }).ToList();

            return orderHistory;
        }

        private int GetTotalQuantityForOrder(int orderId)
        {
            var orderItems = _productDbContext.OrderItem.Where(oi => oi.OrdersId == orderId).ToList();
            return orderItems.Sum(oi => oi.Quantity);
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
            user.PasswordHash = updatedUser.PasswordHash;
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
