using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.DTOs.AiChatHistory;
using VisionOfChosen_BE.Infra.Consts;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIChatHistoryController : AuthorizeController
    {
        private readonly IAIChatHistoryService _chatService;

        public AIChatHistoryController(IAIChatHistoryService chatService)
        {
            _chatService = chatService;
        }

        // GET: api/aichathistory?sessionId=abc123
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string sessionId)
        {
            var chats = await _chatService.GetAllAsync(sessionId, UserHeader.UserId);
            return Ok(chats);
        }

        // GET: api/aichathistory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var chat = await _chatService.GetByIdAsync(id, UserHeader.UserId);
            if (chat == null)
                return NotFound();

            return Ok(chat);
        }

        // POST: api/aichathistory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AiChatHistoryCreateDto dto)
        {
            var result = await _chatService.CreateAsync(dto, UserHeader.UserId, UserHeader.Role);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/aichathistory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AiChatHistoryUpdateDto dto)
        {
            var result = await _chatService.UpdateAsync(id, dto, UserHeader.UserId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE: api/aichathistory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _chatService.DeleteAsync(id, UserHeader.UserId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("sessions")]
        public async Task<IActionResult> GetChatSessions()
        {
            var sessions = await _chatService.GetChatSessionsAsync(UserHeader.UserId);
            return Ok(sessions);
        }

        [HttpGet("new-session")]
        public async Task<IActionResult> GetNewChatSession()
        {
            var sessions = await _chatService.GetNewChatSessionAsync();
            return Ok(sessions);
        }
    }
}
