using MessageProcessing.Shared.Mpa.Mpapi.Dto;

namespace MessageApiService.Mpa.Mpapi.Services
{
    public interface IMessageService
    {
        Task ProcessMessageAsync(MessageDto message);
    }
}
