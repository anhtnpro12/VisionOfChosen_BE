namespace VisionOfChosen_BE.DTOs.AiChatHistory
{
    public class AiChatHistoryDto
    {
        public string Id { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

}
