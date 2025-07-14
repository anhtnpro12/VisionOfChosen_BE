using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.DTOs.AiChatHistory;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIChatHistoryController : ControllerBase
    {
        private readonly IAIChatHistoryService _chatService;

        public AIChatHistoryController(IAIChatHistoryService chatService)
        {
            _chatService = chatService;
        }

        // GET: api/aichathistory?userId=abc123
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string userId)
        {
            var chats = await _chatService.GetAllAsync(userId);
            return Ok(chats);
        }

        // GET: api/aichathistory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var chat = await _chatService.GetByIdAsync(id);
            if (chat == null)
                return NotFound();

            return Ok(chat);
        }

        // POST: api/aichathistory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AiChatHistoryCreateDto dto)
        {
            // ActorId có thể là từ JWT/Token, tạm hard-code ở đây
            string actorId = dto.UserId;
            var result = await _chatService.CreateAsync(dto, actorId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/aichathistory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AiChatHistoryUpdateDto dto)
        {
            string actorId = "system"; // Hoặc từ context
            var result = await _chatService.UpdateAsync(id, dto, actorId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE: api/aichathistory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string actorId = "system"; // Thay bằng thông tin người dùng đăng nhập
            var success = await _chatService.DeleteAsync(id, actorId);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
