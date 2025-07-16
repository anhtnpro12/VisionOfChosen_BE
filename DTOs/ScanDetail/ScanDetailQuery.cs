namespace VisionOfChosen_BE.DTOs.ScanDetail
{
    public class ScanDetailQuery
    {
        public string? FileName { get; set; }
        public string? Status { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
