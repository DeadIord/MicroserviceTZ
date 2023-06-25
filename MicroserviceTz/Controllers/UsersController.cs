using Microsoft.AspNetCore.Mvc;
using MicroserviceTz.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using MicroserviceTz.Data;
using System.Linq;

namespace MicroserviceTz.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AddDbContext _dbContext;

        public UsersController(UserService userService,AddDbContext dbContext)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        [HttpPost("authenticate")]
         
        public IActionResult Authenticate([FromBody] Authentication model, string username, string password)

        {
            model.Username = username;
            model.Password = password;
            var response = _userService.Authenticate(model.Username, model.Password);

            if (response == null)
                return BadRequest(new { message = "Неверные учетные данные" });

            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var products = _userService.GetAllUser();

            if (products == null)
                return NotFound();

            return Ok(products);
        }
  

        [HttpGet("{userId}/ordersHistory")]
        public IActionResult GetOrderHistory(int userId)
        {
            var orderHistory = _userService.GetOrderHistoryForUser(userId);
            if (orderHistory == null)
                return BadRequest(new { message = "Данные отсутствуют" });
            return Ok(orderHistory);
        }
      

        [HttpGet("{userId}/ordersDetails")]
        public IActionResult GetOrderDetails(int orderId)
        {
            var orderHistory = _userService.GetOrderDetails(orderId);
            if (orderHistory == null) 
                return BadRequest(new { message = "Данные отсутствуют" });
            return Ok(orderHistory);
        }


        [HttpPost]

        public IActionResult Create(User user, string username, string password)
        {
            if (_dbContext.Users.Any(x => x.Username == username))
            {
                return BadRequest(new { message = "Пользователь с таким логином уже существует" });
            }
            string passwordHash = HashPassword(password);
            user.PasswordHash = passwordHash;
            user.Username = username;
            user.Token = null;
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
     
        public IActionResult UpdateUser(int userId, string username, string password, User updatedUser)
        {
            if (_dbContext.Users.Any(x => x.Username == username))
            {
                return BadRequest(new { message = "Пользователь с таким логином уже существует" });
            }
            string passwordHash = HashPassword(password);
            updatedUser.PasswordHash = passwordHash;
            updatedUser.Username = username;
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
