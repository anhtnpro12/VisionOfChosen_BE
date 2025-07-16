namespace VisionOfChosen_BE.DTOs.ScanDetail
{
    public class ScanDetailDto
    {
        public string? Id { get; set; }
        public string? FileName { get; set; }
        public DateTime ScanDate { get; set; }
        public string? Status { get; set; }
        public int TotalResources { get; set; }
        public int DriftCount { get; set; }
        public string? RiskLevel { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public List<DriftDto> Drifts { get; set; } = new();
    }
}
