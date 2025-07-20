namespace VisionOfChosen_BE.DTOs.FileStorage
{
    public class FileUploadResult
    {
        public string OriginalFileName { get; set; } = default!;
        public string SavedFileName { get; set; } = default!;
        public long Size { get; set; }
        public string ContentType { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string? Error { get; set; } // null nếu không có lỗi
    }
}
