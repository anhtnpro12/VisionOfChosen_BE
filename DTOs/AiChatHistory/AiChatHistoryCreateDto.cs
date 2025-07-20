using VisionOfChosen_BE.DTOs.FileStorage;

namespace VisionOfChosen_BE.DTOs.AiChatHistory
{
    public class AiChatHistoryCreateDto
    {
        public string SessionId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public List<FileUploadResult> Files { get; set; } = new List<FileUploadResult>();
    }

}
