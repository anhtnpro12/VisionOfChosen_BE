namespace VisionOfChosen_BE.DTOs.AiChatHistory
{
    public class AiChatHistoryUpdateDto
    {
        public string SessionId { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

}
