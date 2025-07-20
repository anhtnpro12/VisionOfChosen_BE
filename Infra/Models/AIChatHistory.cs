using Newtonsoft.Json;
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

        [Column("role")]
        public string Role { get; set; } = "user"; 

        [Column("message")]
        public string Message { get; set; } = string.Empty;

        [Column("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [Column("json_files")]
        public string? JsonFiles { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<FileBase> Files
        {
            get => JsonConvert.DeserializeObject<List<FileBase>>(JsonFiles ?? "[]") ?? new List<FileBase>();
            set => JsonFiles = JsonConvert.SerializeObject(value);
        }
    }
}
