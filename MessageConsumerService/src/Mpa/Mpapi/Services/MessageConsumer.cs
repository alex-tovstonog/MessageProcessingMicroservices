using AutoMapper;
using MassTransit;
using MessageConsumerService.Mpa.Mpapi.Data;
using MessageConsumerService.Mpa.Mpapi.Models;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;

namespace MessageConsumerService.Mpa.Mpapi.Services
{
    public class MessageConsumer : IConsumer<MessageDto>
    {
        private readonly ILogger<MessageConsumer> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageConsumer(ILogger<MessageConsumer> logger,
                               ApplicationDbContext dbContext,
                               IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<MessageDto> context)
        {
            var messageDto = context.Message;

            _logger.LogInformation("Consumed message from RabbitMQ: {Message}", messageDto);

            _logger.LogTrace("Saving message: {Message}", messageDto);

            var message = _mapper.Map<Message>(messageDto);
            message.Id = Guid.NewGuid();
            message.CreatedAt = DateTime.UtcNow;

            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Message saved with ID: {Id}", message.Id);

        }
    }
}
