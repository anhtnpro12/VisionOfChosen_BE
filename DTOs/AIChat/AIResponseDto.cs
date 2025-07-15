using System.Text.Json.Serialization;

namespace VisionOfChosen_BE.DTOs.AIChat
{
    public class AIResponseDto
    {
        [JsonPropertyName("session_id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("response")]
        public string? Response { get; set; }

        [JsonPropertyName("routed_agent")]
        public string? RoutedAgent { get; set; }

        [JsonPropertyName("agent_result")]
        public string? AgentResult { get; set; }

        [JsonPropertyName("conversation_state")]
        public string? ConversationState { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("suggestions")]
        public List<string> Suggestions { get; set; } = new List<string>();
    }
}
