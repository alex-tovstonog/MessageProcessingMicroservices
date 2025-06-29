using MessageProcessing.Shared.Mpa.Mpapi.Dto;
using MessageApiService.Mpa.Mpapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageApiService.Mpa.Mpapi.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] MessageDto message)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid message received: {message}");
                return BadRequest(ModelState);
            }

            await _messageService.ProcessMessageAsync(message);

            _logger.LogInformation($"Message processed successfully: {message}");
            return Accepted(new { message = "Message accepted for processing" });
        }
    }
}
