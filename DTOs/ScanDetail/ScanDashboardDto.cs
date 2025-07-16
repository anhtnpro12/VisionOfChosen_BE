namespace VisionOfChosen_BE.DTOs.ScanDetail
{
    public class ScanDashboardDto
    {
        public LatestScanDto? LatestScan { get; set; }
        public ScanSummaryDto ScanSummary { get; set; } = new();
        public List<ScanHistoryItemDto> ScanHistory { get; set; } = new();
    }

    public class LatestScanDto
    {
        public string? Id { get; set; }
        public string? FileName { get; set; }
        public DateTime ScanDate { get; set; }
        public int DriftCount { get; set; }
        public string? RiskLevel { get; set; }
        public int Warnings { get; set; }
        public string? Status { get; set; }
        public string? Duration { get; set; }
        public int ResourcesScanned { get; set; }
        public int ChangesDetected { get; set; }
    }

    public class ScanSummaryDto
    {
        public int TotalScans { get; set; }
        public int SuccessfulScans { get; set; }
        public int FailedScans { get; set; }
        public double AvgDriftCount { get; set; }
        public int TotalDriftsFound { get; set; }
        public int CriticalIssues { get; set; }
    }

    public class ScanHistoryItemDto
    {
        public string? Id { get; set; }
        public string? FileName { get; set; }
        public DateTime ScanDate { get; set; }
        public string? Status { get; set; }
        public string? RiskLevel { get; set; }
        public int DriftCount { get; set; }
        public int Warnings { get; set; }
        public int ResourcesScanned { get; set; }
        public int ChangesDetected { get; set; }
        public string? Duration { get; set; } 
    }

}
