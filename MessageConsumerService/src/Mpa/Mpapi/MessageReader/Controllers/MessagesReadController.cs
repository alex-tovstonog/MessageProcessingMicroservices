using MessageConsumerService.src.Mpa.Mpapi.MessageReader.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageConsumerService.src.Mpa.Mpapi.MessageReader.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesReadController : ControllerBase
    {
        private readonly IMessageQueryService _queryService;

        public MessagesReadController(IMessageQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var result = await _queryService.GetLatestMessagesAsync();
            return Ok(result);
        }
    }
}
