namespace VisionOfChosen_BE.Infra.Models
{
    public class FileBase
    {
        public string OriginalFileName { get; set; } = default!;
        public string SavedFileName { get; set; } = default!;
        public long Size { get; set; }
        public string ContentType { get; set; } = default!;
        public string Url { get; set; } = default!;
    }
}
