using MassTransit;
using Messaging.RabbitMq.Domain.Shared.Models;

namespace Messaging.RabbitMq.Consumer.Consumers
{
    public class TicketConsumer : IConsumer<TicketModel>
    {
        private readonly ILogger<TicketConsumer> logger;
        public TicketConsumer(ILogger<TicketConsumer> logger) => this.logger = logger;

        public async Task Consume(ConsumeContext<TicketModel> context)
        {
            await Console.Out.WriteLineAsync(context.Message.ClientName);

            logger.LogInformation($"Nova mensagem reecebida : " + $"{context.Message.ClientName} {context.Message.Location}");
        }
    }
}

