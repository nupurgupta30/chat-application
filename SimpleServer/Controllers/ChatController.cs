using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleServer.Data;
using SimpleServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatDbContext _context;

        // Constructor to inject the DbContext
        public ChatController(ChatDbContext context)
        {
            _context = context;
        }

        // GET: api/chats?user1=riya&user2=priya
        [HttpGet]
        public async Task<ActionResult<List<Chat>>> GetChatHistory(string user1, string user2)
        {
            // Validate that both users are provided
            if (string.IsNullOrEmpty(user1) || string.IsNullOrEmpty(user2))
            {
                return BadRequest("Both user1 and user2 must be provided.");
            }

            // Fetch user IDs for the given usernames
            var user1Id = await _context.Users
                .Where(u => u.Username == user1)
                .Select(u => u.UserId)
                .FirstOrDefaultAsync();

            var user2Id = await _context.Users
                .Where(u => u.Username == user2)
                .Select(u => u.UserId)
                .FirstOrDefaultAsync();

            if (user1Id == 0 || user2Id == 0)
            {
                return NotFound("One or both users not found.");
            }

            // Fetch the chat history between the two users, ordered by timestamp
            var chatHistory = await _context.Messages
                .Where(m => (m.SenderId == user1Id && m.RecipientId == user2Id) || (m.SenderId == user2Id && m.RecipientId == user1Id))
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    Sender = m.SenderId == user1Id ? user1 : user2,
                    Receiver = m.RecipientId == user2Id ? user2 : user1,
                    m.MessageText,
                    m.Timestamp
                })
                .ToListAsync();

            // If no chat history is found, return a not found response
            if (chatHistory == null || !chatHistory.Any())
            {
                return NotFound("No chat history found between the users.");
            }

            // Return the chat history as a successful response
            return Ok(chatHistory);
        }
    }
}
