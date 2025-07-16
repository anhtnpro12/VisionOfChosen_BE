using System.ComponentModel.DataAnnotations.Schema;

namespace VisionOfChosen_BE.Infra.Models
{
    [Table("ai_chat_history")]
    public class AiChatHistory : AiChatHistoryProperties
    {
    }

    public class AiChatHistoryProperties : ExtendModel
    {
        [Column("session_id")]
        public string SessionId { get; set; } = string.Empty; // Mỗi phiên chat có thể gồm nhiều lượt hỏi-đáp

        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;

        [Column("role")]
        public string Role { get; set; } = "user"; // "user" hoặc "ai"

        [Column("message")]
        public string Message { get; set; } = string.Empty;

        [Column("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
