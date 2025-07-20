using System.Text.Json.Serialization;

namespace VisionOfChosen_BE.DTOs.AIChat
{
    public class AISetCredentialResponseDto
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("session_id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("aws_region")]
        public string? AwsRegion { get; set; }

        [JsonPropertyName("aws_access_key_id")]
        public string? AwsAccessKeyId { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
