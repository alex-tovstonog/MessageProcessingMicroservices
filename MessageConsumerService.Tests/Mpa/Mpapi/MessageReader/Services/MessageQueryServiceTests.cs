using AutoMapper;
using FluentAssertions;
using MessageConsumerService.Mpa.Mpapi.Data;
using MessageConsumerService.Mpa.Mpapi.Models;
using MessageConsumerService.src.Mpa.Mpapi.MessageReader.Services;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace MessageConsumerService.Tests.Mpa.Mpapi.MessageReader.Services
{
    public class MessageQueryServiceTests
    {
        private ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetLatestMessagesAsync_ReturnsMappedMessages()
        {
            // Arrange
            var dbContext = CreateDbContext();

            dbContext.Messages.AddRange(
                new Message { Id = Guid.NewGuid(), Text = "Old", CreatedAt = DateTime.UtcNow.AddMinutes(-10) },
                new Message { Id = Guid.NewGuid(), Text = "New", CreatedAt = DateTime.UtcNow }
            );
            await dbContext.SaveChangesAsync();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<MessageDto>>(It.IsAny<IEnumerable<Message>>()))
                      .Returns<IEnumerable<Message>>(messages => messages.Select(m => new MessageDto
                      {
                          Text = m.Text
                      }));

            var service = new MessageQueryService(dbContext, mockMapper.Object);

            // Act
            var result = await service.GetLatestMessagesAsync();

            // Assert
            result.Should().HaveCount(2);
            result.First().Text.Should().Be("New");
        }
    }
}
