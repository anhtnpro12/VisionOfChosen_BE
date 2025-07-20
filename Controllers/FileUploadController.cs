using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : AuthorizeController
    {
        private readonly IFileStorageService _fileStorageService;

        public FileUploadController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
                return BadRequest("File is required.");

            try
            {
                var path = await _fileStorageService.SaveFileAsync(file);
                return Ok(new { fileName = file.FileName, savedPath = path });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"File upload failed: {ex.Message}");
            }
        }

        [HttpPost("upload-multiple")]
        public async Task<IActionResult> UploadMultiple(List<IFormFile> files)
        {
            if (files == null || !files.Any())
                return BadRequest("No files uploaded.");

            var results = await _fileStorageService.SaveFilesAsync(files);

            //var response = results.Select(r => new
            //{
            //    r.OriginalFileName,
            //    r.SavedFileName,
            //    r.Size,
            //    r.ContentType,
            //    url = string.IsNullOrEmpty(r.Error) ? $"{Request.Scheme}://{Request.Host}{r.Url}" : null,
            //    error = r.Error
            //});

            return Ok(results);
        }

    }
}
