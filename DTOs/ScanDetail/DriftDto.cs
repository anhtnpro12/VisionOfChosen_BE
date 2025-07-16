namespace VisionOfChosen_BE.DTOs.ScanDetail
{
    public class DriftDto
    {
        public string? DriftCode { get; set; }
        public string? ResourceType { get; set; }
        public string? ResourceName { get; set; }
        public string? RiskLevel { get; set; }
        public object? BeforeStateJson { get; set; }
        public object? AfterStateJson { get; set; }
        public string? AiExplanation { get; set; }
        public string? AiAction { get; set; }
    }
}
