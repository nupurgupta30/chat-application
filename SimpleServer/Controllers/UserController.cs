using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleServer.Data;
using SimpleServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ChatDbContext _context;

        // Constructor to inject the DbContext
        public UserController(ChatDbContext context)
        {
            _context = context;
        }

        // GET: api/user?exclude={username}
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetUsers([FromQuery] string exclude)
        {
            if (string.IsNullOrEmpty(exclude))
            {
                return BadRequest("The 'exclude' query parameter is required.");
            }

            // Fetch users from the database, excluding the one passed in the 'exclude' parameter
            var users = await _context.Users
                                       .Where(u => u.Username != exclude)
                                       .Select(u => u.Username)
                                       .ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }
    }
}
