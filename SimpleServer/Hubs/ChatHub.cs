using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;
using SimpleServer.Data;
using SimpleServer.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SimpleServer.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _connectionString;

        // Store username-to-ConnectionId mapping
        private static readonly ConcurrentDictionary<string, string> UserConnections = new();

        public ChatHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine("ChatHub initialized with connection string: " + _connectionString);
        }

        public override async Task OnConnectedAsync()
        {
            var username = Context.GetHttpContext()?.Request.Query["username"];

            if (!string.IsNullOrEmpty(username))
            {
                // Add or update user-connection mapping
                UserConnections[username] = Context.ConnectionId;
                Console.WriteLine($"User {username} connected with ConnectionId: {Context.ConnectionId}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception? exception)
        {
            // Remove the user from the connection mapping
            var username = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (!string.IsNullOrEmpty(username))
            {
                UserConnections.TryRemove(username, out _);
                Console.WriteLine($"User {username} disconnected.");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string sender, string receiver, string message)
        {
            Console.WriteLine($"Sending message from {sender} to {receiver}: {message}");

            // Ensure sender and receiver exist in the database
            var senderId = await GetUserIdByUsername(sender);
            var receiverId = await GetUserIdByUsername(receiver);

            if (senderId == null || receiverId == null)
            {
                Console.WriteLine("Error: User(s) not found.");
                return;
            }

            // Save the message to the 'messages' table
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO messages (SenderId, RecipientId, MessageText, Timestamp) " +
                            "VALUES (@SenderId, @RecipientId, @MessageText, NOW())";
                await connection.ExecuteAsync(query, new
                {
                    SenderId = senderId,
                    RecipientId = receiverId,
                    MessageText = message
                });
                Console.WriteLine("Message saved to database.");
            }

            // Notify both the sender and receiver
            if (UserConnections.TryGetValue(sender, out var senderConnectionId))
            {
                await Clients.Client(senderConnectionId).SendAsync("ReceiveMessage", sender, message);
            }

            if (UserConnections.TryGetValue(receiver, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", sender, message);
            }

            Console.WriteLine($"Message sent to {sender} and {receiver}.");
        }

        private async Task<int?> GetUserIdByUsername(string username)
        {
            Console.WriteLine($"Getting user ID for username: {username}");
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT UserId FROM users WHERE Username = @Username";
                var userId = await connection.QueryFirstOrDefaultAsync<int?>(query, new { Username = username });
                if (userId != null)
                {
                    Console.WriteLine($"User {username} found with ID: {userId}");
                }
                else
                {
                    Console.WriteLine($"User {username} not found.");
                }
                return userId;
            }
        }
    }
}
