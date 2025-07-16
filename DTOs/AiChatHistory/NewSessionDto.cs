using System.Text.Json.Serialization;

namespace VisionOfChosen_BE.DTOs.AiChatHistory
{
    public class NewSessionDto
    {
        [JsonPropertyName("session_id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("conversation_state")]
        public string? ConversationState { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("suggestions")]
        public List<string> Suggestions { get; set; }
    }
}
