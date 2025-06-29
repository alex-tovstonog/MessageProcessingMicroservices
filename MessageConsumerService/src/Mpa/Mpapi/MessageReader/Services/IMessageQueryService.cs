using MessageProcessing.Shared.Mpa.Mpapi.Dto;

namespace MessageConsumerService.src.Mpa.Mpapi.MessageReader.Services
{
    public interface IMessageQueryService
    {
        Task<IEnumerable<MessageDto>> GetLatestMessagesAsync();
    }
}
