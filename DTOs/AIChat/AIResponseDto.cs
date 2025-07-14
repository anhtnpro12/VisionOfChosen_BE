namespace VisionOfChosen_BE.DTOs.AIChat
{
    public class AIResponseDto
    {
        public string Status { get; set; } = string.Empty;
        public string Reply { get; set; } = string.Empty;
    }

    public class AIReplyParsed
    {
        public string Role { get; set; } = string.Empty;
        public List<AIReplyContent> Content { get; set; } = new();
    }

    public class AIReplyContent
    {
        public string Text { get; set; } = string.Empty;
    }

}
