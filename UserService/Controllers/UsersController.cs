using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _userService;
        private readonly AddDbContext _dbContext;

        public UsersController(UserServices userService,AddDbContext dbContext)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        [HttpPost("authenticate")]

        public async Task<IActionResult> AuthenticateAsync([FromBody] Authentication model, string username, string password)

        {
            model.Username = username;
            model.Password = password;
            var response = await _userService.AuthenticateAsync(model.Username, model.Password);

            if (response == null)
                return BadRequest(new { message = "Неверные учетные данные" });

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var products = await _userService.GetAllUserAsync();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{userId}/ordersHistory")]
        public async Task<IActionResult> GetOrderHistoryAsync(int userId)
        {
            var orderHistory = await _userService.GetOrderHistoryForUserAsync(userId);
            if (orderHistory.Count == 0)
                return BadRequest(new { message = "Данные отсутствуют" });
            return Ok(orderHistory);
        }

        [HttpGet("{userId}/ordersDetails")]
        public async Task<IActionResult> GetOrderDetailsAsync(int orderId, int userId)
        {
            var orderDetails = await _userService.GetOrderDetailsAsync(orderId, userId);
            if (orderDetails == null)
                return BadRequest(new { message = "Данные отсутствуют" });
            return Ok(orderDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string username, string password)
        {
            if (_dbContext.Users.Any(x => x.Username == username))
            {
                return BadRequest(new { message = "Пользователь с таким логином уже существует" });
            }
            string passwordHash = HashPassword(password);
            var user = new User() { Username = username, PasswordHash = passwordHash };
            var createdUser = _userService.Create(user);
            if (createdUser == null)
                return BadRequest(new { message = "Некорректны данные" });

            return CreatedAtAction(nameof(GetUser), new { userId = createdUser.Id }, createdUser);
        }



        private string HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }


        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            var user = _userService.GetById(userId);

            if (user == null)
                return BadRequest(new { message = "Данный пользователь отсутствует" });

            return Ok(user);
        }

     
        [HttpPut("{userId}")]
     
        public IActionResult UpdateUser(int userId, string username, string password)
        {
            if (_dbContext.Users.Any(x => x.Username == username))
            {
                return BadRequest(new { message = "Пользователь с таким логином уже существует" });
            }
            string passwordHash = HashPassword(password);
           var updatedUser = new User() { Username = username, PasswordHash = passwordHash };
            var user = _userService.Update(userId, updatedUser);

            if (user == null)
                return BadRequest(new { message = "Данный пользователь отсутствует" });

            return Ok(user);
        }

 
        [HttpDelete("{userId}")]
   
        public IActionResult DeleteUser(int userId)
        {
            var user = _userService.Delete(userId);

            if (user == null)
                return BadRequest(new { message = "Данный пользователь отсутствует" });

            return NoContent();
        }
    }
}
