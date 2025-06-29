namespace MessageConsumerService.Mpa.Mpapi.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Text { get; set; }
        public string? Optional { get; set; }
        public DateOnly Date { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
