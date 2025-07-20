using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.Infra.Models;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingController : AuthorizeController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMySettings()
        {
            var settings = await _settingService.GetSettingsAsync(UserHeader.UserId);
            return Ok(settings);
        }
    }
}
