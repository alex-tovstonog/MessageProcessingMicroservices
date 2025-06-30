using MassTransit;
using MessageApiService.Mpa.Mpapi.Services;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessageApiService.Tests.Services
{
    public class RabbitMqMessageServiceTests
    {
        [Fact]
        public async Task ProcessMessageAsync_PublishesMessageToBus()
        {
            var loggerMock = new Mock<ILogger<RabbitMqMessageService>>();
            var busMock = new Mock<IPublishEndpoint>();

            var service = new RabbitMqMessageService(busMock.Object, loggerMock.Object);

            var message = new MessageDto
            {
                Number = 1,
                Text = "Test",
                Optional = "abc",
                Date = new DateOnly(2025, 6, 27)
            };


            await service.ProcessMessageAsync(message);


            busMock.Verify(bus => bus.Publish(message, default), Times.Once);
        }
    }
}
