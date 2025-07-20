using VisionOfChosen_BE.DTOs.FileStorage;

namespace VisionOfChosen_BE.DTOs.Setting
{
    public class AwsCredentialCreateDto
    {
        public string? AwsAccessKeyId { get; set; }
        public string? AwsSecretAccessKey { get; set; }
        public string? AwsRegion { get; set; }
        public FileUploadResult PemFile { get; set; } = new();
    }
}
