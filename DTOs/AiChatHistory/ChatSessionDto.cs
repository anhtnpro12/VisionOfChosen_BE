namespace VisionOfChosen_BE.DTOs.AiChatHistory
{
    public class ChatSessionDto
    {
        public string SessionId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastActivity { get; set; }
        public string Preview { get; set; } = string.Empty;
        public int MessageCount { get; set; }
    }
}
