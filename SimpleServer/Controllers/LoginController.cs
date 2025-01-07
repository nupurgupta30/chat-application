using Microsoft.AspNetCore.Mvc;
using Pomelo.EntityFrameworkCore.MySql;
using Dapper;
//using MySql.Data.MySqlClient;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace SimpleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string _connectionString;

        public LoginController(IConfiguration configuration)
        {
            // Fetch the connection string from appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid request.");
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))  // Using Pomelo connection
                {
                    connection.Open();

                    var query = "SELECT * FROM Users WHERE Username = @Username";
                    var user = connection.QueryFirstOrDefault<User>(query, new { Username = loginRequest.Username });

                    if (user != null)
                    {
                        bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);

                        if (passwordMatch)
                        {
                            return Ok(user);  // Return user data if authenticated successfully
                        }
                        else
                        {
                            return Unauthorized("Invalid username or password.");
                        }
                    }
                    else
                    {
                        return Unauthorized("Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}

