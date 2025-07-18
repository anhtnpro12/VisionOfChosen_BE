namespace VisionOfChosen_BE.Authentication
{
    public class UserHeader
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Name { get; set; }
    }
}
