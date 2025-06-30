using AutoMapper;
using FluentAssertions;
using MassTransit;
using MessageConsumerService.Mpa.Mpapi.Data;
using MessageConsumerService.Mpa.Mpapi.Models;
using MessageConsumerService.Mpa.Mpapi.Services;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessageConsumerService.Tests.Mpa.Mpapi.Services
{
    public class MessageConsumerTests
    {
            private ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Consume_ValidMessage_SavesToDatabase()
    {
        // Arrange
        var messageDto = new MessageDto
        {
            Number = 1,
            Text = "Test",
            Optional = "opt",
            Date = new DateOnly(2025, 6, 27)
        };

        var messageEntity = new Message
        {
            Id = Guid.NewGuid(),
            Number = messageDto.Number,
            Text = messageDto.Text,
            Optional = messageDto.Optional,
            Date = messageDto.Date,
            CreatedAt = DateTime.UtcNow
        };

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Message>(messageDto)).Returns(messageEntity);

        var loggerMock = new Mock<ILogger<MessageConsumer>>();

        var dbContext = CreateInMemoryDbContext();

        var contextMock = new Mock<ConsumeContext<MessageDto>>();
        contextMock.Setup(c => c.Message).Returns(messageDto);

        var consumer = new MessageConsumer(loggerMock.Object, dbContext, mapperMock.Object);

        // Act
        await consumer.Consume(contextMock.Object);

        // Assert
        var messages = await dbContext.Messages.ToListAsync();
        messages.Should().ContainSingle();
        messages[0].Text.Should().Be("Test");
        mapperMock.Verify(m => m.Map<Message>(messageDto), Times.Once);
    }


    }
}
