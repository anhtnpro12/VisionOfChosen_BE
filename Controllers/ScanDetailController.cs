using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.DTOs.ScanDetail;
using VisionOfChosen_BE.Infra.Consts;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanDetailController : ControllerBase
    {
        private readonly IScanDetailService _service;

        public ScanDetailController(IScanDetailService service)
        {
            _service = service;
        }

        // GET: api/scandetail
        [HttpGet]
        public async Task<ActionResult<List<ScanDetailDto>>> GetList([FromQuery] ScanDetailQuery query)
        {
            var result = await _service.GetListAsync(query);
            return Ok(result);
        }

        // GET: api/scandetail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ScanDetailDto>> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/scandetail
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] ScanDetailDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // PUT: api/scandetail/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ScanDetailDto>> Update(string id, [FromBody] ScanDetailDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // DELETE: api/scandetail/{id}?deletedBy=user123
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var success = await _service.DeleteAsync(id, RoleConst.userIdDefault);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<ScanDashboardDto>> GetDashboard()
        {
            var raw = await _service.GetScanDashboardAsync();
            return Ok(raw);
        }
    }

}
