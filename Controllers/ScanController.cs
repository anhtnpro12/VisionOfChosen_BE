using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.DTOs.Scan;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanController : ControllerBase
    {
        private readonly IScanService _service;

        public ScanController(IScanService service)
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
        public async Task<IActionResult> Create([FromBody] ScanCreateDto dto)
        {
            var actorId = "system"; // có thể lấy từ JWT
            var result = await _service.CreateAsync(dto, actorId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ScanUpdateDto dto)
        {
            var actorId = "system"; // có thể lấy từ JWT
            var result = await _service.UpdateAsync(id, dto, actorId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var actorId = "system";
            var success = await _service.DeleteAsync(id, actorId);
            return success ? NoContent() : NotFound();
        }
    }
}
