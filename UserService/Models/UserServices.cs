using UserService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UserService.ViewModel;
using System.Threading.Tasks;
using MassTransit;
using OrderService.Core.Commands;
using MassTransit.Clients;
using Microsoft.Extensions.Logging;

namespace UserService.Models
{
    public class UserServices
    {
        private readonly AddDbContext _dbContext;
        private readonly IRequestClient<RequestForOrderHistory> _getProductsRequestClient;
        private readonly ILogger<UserServices> _logger;

        public UserServices(AddDbContext dbContext, IRequestClient<RequestForOrderHistory> getProductsRequestClient, ILogger<UserServices> logger)
        {
            _dbContext = dbContext;
            _getProductsRequestClient = getProductsRequestClient;
            _logger = logger;
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

        public async Task<OrderDetailsVM> GetOrderDetailsAsync(int orderId, int userId)
        {
            _logger.LogInformation("Получение деталей заказа для пользователя {UserId}, заказ {OrderId}", userId, orderId);

            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                _logger.LogWarning("Заказ с Id {OrderId} для пользователя {UserId} не найден", orderId, userId);
                return null;
            }

            var productIds = order.OrderItems.Select(oi => oi.ProductId).ToList();

            _logger.LogInformation("Отправка запроса на получение информации о продуктах по Ids: {ProductIds}", string.Join(", ", productIds));

            var getProductsRequest = new RequestForOrderHistory
            {
                ProductIds = productIds
            };

            var getProductsResponse = await _getProductsRequestClient.GetResponse<GetProductsResponse>(getProductsRequest);

            var productsInfo = getProductsResponse.Message.Products;

            var orderDetails = new OrderDetailsVM
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalCost = order.TotalCost,
                OrderItems = order.OrderItems.Select(oi => new OrderItemVM
                {
                    ProductName = productsInfo.FirstOrDefault(p => p.Id == oi.ProductId)?.Name,
                    Quantity = oi.Quantity
                }).ToList()
            };

            _logger.LogInformation("Получены детали заказа для пользователя {UserId}, заказ {OrderId}", userId, orderId);

            return orderDetails;
        }


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
