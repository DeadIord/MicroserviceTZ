using UserService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UserService.ViewModel;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class UserServices
    {
        private readonly AddDbContext _dbContext;
        


        public UserServices(AddDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null || VerifyPasswordHash(password, user.PasswordHash))
                return null;

            user.Token = GenerateToken();

            _dbContext.SaveChanges();

            return user;
        }
        public async Task<List<User>> GetAllUserAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        //public async Task<OrderDetailsVM> GetOrderDetailsAsync(int orderId, int userId)
        //{
        //    var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        //    if (order == null)
        //        return null;

        //    var orderItems = await _productDbContext.OrderItem
        //        .Where(oi => oi.OrdersId == orderId)
        //        .Include(oi => oi.Product)
        //        .ToListAsync();

        //    var orderDetails = new OrderDetailsVM
        //    {
        //        OrderId = order.Id,
        //        OrderDate = order.OrderDate,
        //        TotalCost = order.TotalCost,
        //        OrderItems = orderItems.Select(oi => new OrderItemVM
        //        {
        //            ProductName = oi.Product.Name,
        //            Quantity = oi.Quantity
        //        }).ToList()
        //    };

        //    return orderDetails;
        //}
        //public async Task<List<OrderHistoryItemVM>> GetOrderHistoryForUserAsync(int userId)
        //{
        //    var orderHistory =await  _dbContext.Orders
        //        .Where(o => o.UserId == userId)
        //        .Select(o => new OrderHistoryItemVM
        //        {
        //            OrderId = o.Id,
        //            OrderDate = o.OrderDate,
        //            TotalCost = o.TotalCost,
        //            TotalQuantity = GetTotalQuantityForOrder(o.Id)
        //        })
        //        .ToListAsync();

        //    return orderHistory;
        //}
        //private int GetTotalQuantityForOrder(int orderId)
        //{
        //        return _productDbContext.OrderItem.Where(oi => oi.OrdersId == orderId).Sum(oi => oi.Quantity);
        //}
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
