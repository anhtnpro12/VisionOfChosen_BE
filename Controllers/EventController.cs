using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.DTOs.Event;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventCreateDto dto)
        {
            var actorId = dto.UserId.ToString(); // hoặc lấy từ JWT
            var result = await _service.CreateAsync(dto, actorId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] EventUpdateDto dto)
        {
            var actorId = "system"; // hoặc lấy từ JWT
            var result = await _service.UpdateAsync(id, dto, actorId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var actorId = "system"; // hoặc lấy từ JWT
            var success = await _service.DeleteAsync(id, actorId);
            return success ? NoContent() : NotFound();
        }
    }
}
