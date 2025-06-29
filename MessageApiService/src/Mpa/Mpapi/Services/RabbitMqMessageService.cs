using MassTransit;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;

namespace MessageApiService.Mpa.Mpapi.Services
{
    public class RabbitMqMessageService : IMessageService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<RabbitMqMessageService> _logger;

        public RabbitMqMessageService(IPublishEndpoint publishEndpoint, ILogger<RabbitMqMessageService> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task ProcessMessageAsync(MessageDto message)
        {
            _logger.LogInformation("Publishing message to RabbitMQ: {Message}", message);

            await _publishEndpoint.Publish(message);
        }
    }
}
