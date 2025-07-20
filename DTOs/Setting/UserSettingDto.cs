namespace VisionOfChosen_BE.DTOs.Setting
{
    public class UserSettingDto
    {
        public AwsCredentialDto? AwsCredential { get; set; }
        public List<string> Emails { get; set; } = new();
    }
}
