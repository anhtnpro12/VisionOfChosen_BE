using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.DTOs.AIChat;
using VisionOfChosen_BE.Infra.Consts;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIChatController : AuthorizeController
    {
        private readonly IAIChatService _service;

        public AIChatController(IAIChatService service)
        {
            _service = service;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] AIChatRequestDto request)
        {
            var result = await _service.ProcessPromptAsync(request, UserHeader.UserId, UserHeader.Role);
            return Ok(result);
        }

        [HttpPost("set-aws-credentials")]
        public async Task<IActionResult> SetAWSCredentials([FromBody] SetAWSCredentialsRequestDto request)
        {
            var result = await _service.SetAWSCredentials(request, UserHeader.UserId);
            return Ok(result);
        }
    }

}
