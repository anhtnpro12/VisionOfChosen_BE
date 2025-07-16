using System.ComponentModel.DataAnnotations.Schema;

namespace VisionOfChosen_BE.Infra.Models
{
    [Table("scan_detail")]
    public class ScanDetail : ScanDetailProperties
    {
        public ICollection<Drift> Drifts { get; set; } = new List<Drift>();
    }

    public class ScanDetailProperties : ExtendModel
    {
        [Column("file_name")]
        public string? FileName { get; set; }

        [Column("scan_date")]
        public DateTime ScanDate { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("total_resources")]
        public int TotalResources { get; set; }

        [Column("drift_count")]
        public int DriftCount { get; set; }

        [Column("risk_level")]
        public string? RiskLevel { get; set; }

        [Column("duration")]
        public TimeSpan? Duration { get; set; }
    }
}
