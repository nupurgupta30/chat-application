namespace SimpleServer.Models
{
    public class Chat
    {
        public int Id { get; set; }            // Primary key, auto-incremented by EF
        public string Sender { get; set; }     // Sender of the message
        public string Receiver { get; set; }   // Receiver of the message
        public string Message { get; set; }    // The actual message content
        public DateTime Timestamp { get; set; } // Time the message was sent
    }
}
