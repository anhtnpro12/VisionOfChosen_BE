namespace VisionOfChosen_BE.DTOs.Scan
{
    public class ScanDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime ScanTime { get; set; }
        public string? Directory { get; set; }
        public int AddedResources { get; set; }
        public int ChangedResources { get; set; }
        public int DestroyedResources { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }

}
