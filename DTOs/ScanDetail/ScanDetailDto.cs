using System.Text.Json.Serialization;

namespace VisionOfChosen_BE.DTOs.ScanDetail
{
    public class ScanDetailDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("fileName")]
        public string? FileName { get; set; }

        [JsonPropertyName("scanDate")]
        public DateTime ScanDate { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("totalResources")]
        public int TotalResources { get; set; }

        [JsonPropertyName("driftCount")]
        public int DriftCount { get; set; }

        [JsonPropertyName("riskLevel")]
        public string? RiskLevel { get; set; }

        [JsonPropertyName("duration")]
        [JsonIgnore]
        public TimeSpan? Duration { get; set; }

        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime? CreatedOn { get; set; }

        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }

        [JsonPropertyName("drifts")]
        public List<DriftDto> Drifts { get; set; } = new();
    }
}
