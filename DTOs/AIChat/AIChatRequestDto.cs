using VisionOfChosen_BE.DTOs.FileStorage;

namespace VisionOfChosen_BE.DTOs.AIChat
{
    public class AIChatRequestDto
    {
        public string SessionId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<FileUploadResult> Files { get; set; } = new List<FileUploadResult>();
    }

}
