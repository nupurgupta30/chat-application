namespace SimpleServer.Models
{
    public class User
    {
        public int Id { get; set; }           // Primary key, auto-incremented by EF
        public string Username { get; set; }  // Username (unique)
        public string Password { get; set; }  // Password (you should hash this before storing it in the DB)
        public string Email { get; set; }     // User's email address
    }
}
