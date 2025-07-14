namespace VisionOfChosen_BE.DTOs.AIChat
{
    public class AIChatRequestDto
    {
        public string SessionId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

}
