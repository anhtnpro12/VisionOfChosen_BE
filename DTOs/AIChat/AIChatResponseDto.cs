using VisionOfChosen_BE.Infra.Consts;

namespace VisionOfChosen_BE.DTOs.AIChat
{
    public class AIChatResponseDto
    {
        public string Role { get; set; } = RoleConst.AI;
        public string Message { get; set; } = string.Empty;
    }

}
