namespace VisionOfChosen_BE.DTOs.FileStorage
{
    public class FileUploadResult
    {
        public string OriginalFileName { get; set; } = string.Empty;
        public string SavedFileName { get; set; } = string.Empty;
        public long Size { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? Error { get; set; } // null nếu không có lỗi
    }
}
