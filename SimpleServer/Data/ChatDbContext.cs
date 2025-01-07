//using Microsoft.EntityFrameworkCore;
//using SimpleServer.Models;

//namespace SimpleServer.Data
//{
//    public class ChatDbContext : DbContext
//    {
//        /*
//        public ChatDbContext(DbContextOptions<ChatDbContext> options)
//            : base(options) { }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Message> Messages { get; set; }
//        */

//        public ChatDbContext(DbContextOptions<ChatDbContext> options) 
//            : base(options) { }

//        // Add the DbSet<Chat> here
//        public DbSet<Chat> Chats { get; set; }

//        // If you have a Users table as well
//        public DbSet<User> Users { get; set; }
//    }

//    public class User
//    {
//        public int UserId { get; set; }
//        public string Username { get; set; }
//        public string PasswordHash { get; set; }
//        public string Email { get; set; }
//    }

//    public class Message
//    {
//        public int MessageId { get; set; }
//        public int SenderId { get; set; }
//        public int RecipientId { get; set; }
//        public string MessageText { get; set; }
//        public DateTime Timestamp { get; set; }
//        public User Sender { get; set; }
//        public User Recipient { get; set; }
//    }
//}

using Microsoft.EntityFrameworkCore;
using SimpleServer.Models;

namespace SimpleServer.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; } // This is important

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize your database schema if needed, e.g., indexes or constraints
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Message>().HasKey(m => m.MessageId);
            modelBuilder.Entity<Chat>().HasKey(c => c.Id);
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }

    public class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string MessageText { get; set; }
        public DateTime Timestamp { get; set; }
        public User Sender { get; set; }
        public User Recipient { get; set; }
    }

    
}

