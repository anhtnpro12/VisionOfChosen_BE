using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace VisionOfChosen_BE.Infra.Models
{
    [Table("drift")]
    public class Drift : DriftProperties
    {
        public ScanDetail? ScanDetail { get; set; }
    }

    public class DriftProperties : ExtendModel
    {
        [Column("drift_code")]
        public string? DriftCode { get; set; }

        [Column("resource_type")]
        public string? ResourceType { get; set; }

        [Column("resource_name")]
        public string? ResourceName { get; set; }

        [Column("risk_level")]
        public string? RiskLevel { get; set; }

        [Column("before_state_json")]
        public string? BeforeStateJson { get; set; }

        [Column("after_state_json")]
        public string? AfterStateJson { get; set; }

        [Column("ai_explanation")]
        public string? AiExplanation { get; set; }

        [Column("ai_action")]
        public string? AiAction { get; set; }

        [Column("scan_detail_id")]
        public string? ScanDetailId { get; set; }

        [NotMapped]
        public Dictionary<string, string> BeforeState =>
            string.IsNullOrEmpty(BeforeStateJson)
                ? new Dictionary<string, string>()
                : JsonSerializer.Deserialize<Dictionary<string, string>>(BeforeStateJson) ?? new Dictionary<string, string>();

        [NotMapped]
        public Dictionary<string, string> AfterState =>
            string.IsNullOrEmpty(AfterStateJson)
                ? new Dictionary<string, string>()
                : JsonSerializer.Deserialize<Dictionary<string, string>>(AfterStateJson) ?? new Dictionary<string, string>();
    }
}
