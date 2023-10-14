namespace Messaging.RabbitMq.Domain.Shared.Models
{
    public class TicketModel
    {
        public string? ClientName { get; set; }
        public DateTime Booked { get; set; }
        public string? Location { get; set; }
    }
}

