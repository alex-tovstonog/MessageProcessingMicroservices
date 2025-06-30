using FluentAssertions;
using MessageApiService.Mpa.Mpapi.Controllers;
using MessageApiService.Mpa.Mpapi.Services;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessageApiService.Tests.Controllers
{
    public class MessagesControllerTests
    {
        [Fact]
        public async Task Post_ValidMessage_CallsServiceAndReturnsOk()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<MessagesController>>();
            var mockService = new Mock<IMessageService>();
            var controller = new MessagesController(mockLogger.Object, mockService.Object);

            var dto = new MessageDto
            {
                Number = 42,
                Text = "Test message",
                Optional = "Optional field",
                Date = new DateOnly(2025, 6, 27)
            };

            // Act
            var result = await controller.PostMessage(dto);

            // Assert
            result.Should().BeOfType<AcceptedResult>();
            mockService.Verify(s => s.ProcessMessageAsync(dto), Times.Once);
        }
    }
}
