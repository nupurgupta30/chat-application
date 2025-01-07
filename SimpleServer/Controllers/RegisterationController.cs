using Microsoft.AspNetCore.Mvc;
using Pomelo.EntityFrameworkCore.MySql;
using Dapper;  // For easier database operations with Dapper
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace SimpleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly string _connectionString;

        public RegisterController(IConfiguration configuration)
        {
            // Fetch the connection string from appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null ||
                string.IsNullOrEmpty(registerRequest.Username) ||
                string.IsNullOrEmpty(registerRequest.Password) ||
                string.IsNullOrEmpty(registerRequest.Email) ||
                !IsValidEmail(registerRequest.Email))
            {
                return BadRequest("Invalid input.");
            }

            // Hash the password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            using (var connection = new MySqlConnection(_connectionString))  // Using Pomelo connection
            {
                connection.Open();

                // Check if the username already exists
                var checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                int count = connection.QueryFirstOrDefault<int>(checkQuery, new { Username = registerRequest.Username });

                if (count > 0)
                {
                    return BadRequest("Username already exists.");
                }

                // Insert the new user
                var query = "INSERT INTO Users (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email)";
                int rowsAffected = connection.Execute(query, new { Username = registerRequest.Username, PasswordHash = hashedPassword, Email = registerRequest.Email });

                if (rowsAffected > 0)
                {
                    return Ok("User registered successfully.");
                }
                else
                {
                    return BadRequest("Error registering user.");
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
