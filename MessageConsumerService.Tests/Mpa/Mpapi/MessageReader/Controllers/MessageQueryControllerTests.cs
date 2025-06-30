using FluentAssertions;
using MessageConsumerService.src.Mpa.Mpapi.MessageReader.Controllers;
using MessageConsumerService.src.Mpa.Mpapi.MessageReader.Services;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessageConsumerService.Tests.Mpa.Mpapi.MessageReader.Controllers
{
    public class MessageQueryControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkWithLatestMessages()
        {
            // Arrange
            var expectedMessages = new List<MessageDto>
        {
            new MessageDto { Number = 1, Text = "First" },
            new MessageDto { Number = 2, Text = "Second" }
        };

            var mockService = new Mock<IMessageQueryService>();
            mockService.Setup(s => s.GetLatestMessagesAsync())
                       .ReturnsAsync(expectedMessages);

            //var mockLogger = new Mock<ILogger<MessagesReadController>>();
            var controller = new MessagesReadController(mockService.Object);

            // Act
            var result = await controller.GetLatest();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(expectedMessages);
        }
    }
}
