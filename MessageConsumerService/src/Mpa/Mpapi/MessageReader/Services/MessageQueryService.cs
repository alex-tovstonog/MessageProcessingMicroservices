using AutoMapper;
using MessageConsumerService.Mpa.Mpapi.Data;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;
using Microsoft.EntityFrameworkCore;

namespace MessageConsumerService.src.Mpa.Mpapi.MessageReader.Services
{
    public class MessageQueryService : IMessageQueryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageQueryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> GetLatestMessagesAsync()
        {
            var messages = await _dbContext.Messages
                .OrderByDescending(m => m.CreatedAt)
                .Take(100)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }
    }
}
