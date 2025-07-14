using System.Text.Json;
using System.Text;
using VisionOfChosen_BE.DTOs.AiChatHistory;
using VisionOfChosen_BE.DTOs.AIChat;
using VisionOfChosen_BE.Utils;
using VisionOfChosen_BE.Infra.Consts;

namespace VisionOfChosen_BE.Services
{
    public interface IAIChatService
    {
        Task<AIChatResponseDto> ProcessPromptAsync(AIChatRequestDto request);
    }

    public class AIChatService : IAIChatService
    {
        private readonly IHttpHelper _httpHelper;
        private readonly IAIChatHistoryService _historyService;

        public AIChatService(IHttpHelper httpHelper, IAIChatHistoryService historyService)
        {
            _httpHelper = httpHelper;
            _historyService = historyService;
        }

        public async Task<AIChatResponseDto> ProcessPromptAsync(AIChatRequestDto request)
        {
            // 1. Lưu câu hỏi người dùng
            var userMsg = new AiChatHistoryCreateDto
            {
                SessionId = request.SessionId,
                UserId = request.UserId,
                Role = "user",
                Message = request.Message,
                Timestamp = DateTime.UtcNow
            };
            await _historyService.CreateAsync(userMsg, request.UserId);

            // 2. Gửi request lên AI
            var payload = new
            {
                message = request.Message,
                user_id = request.UserId,
                session_id = request.SessionId
            };

            var aiResponse = await _httpHelper.PostJsonAsync<object, AIResponseDto>(
                AiApiRoutes.Chat.Ask, payload);

            // 3. Parse nội dung chuỗi JSON trong "reply"
            var fixedReply = aiResponse.Reply.Replace("'", "\"");
            var replyParsed = JsonSerializer.Deserialize<AIReplyParsed>(fixedReply);

            var text = replyParsed?.Content?.FirstOrDefault()?.Text ?? "[Không có phản hồi]";

            // 4. Lưu phản hồi của AI
            var aiMsg = new AiChatHistoryCreateDto
            {
                SessionId = request.SessionId,
                UserId = request.UserId,
                Role = replyParsed?.Role ?? "assistant",
                Message = text,
                Timestamp = DateTime.UtcNow
            };
            await _historyService.CreateAsync(aiMsg, "ai");

            // 5. Trả kết quả
            return new AIChatResponseDto
            {
                Role = replyParsed?.Role ?? "assistant",
                Message = text
            };
        }
    }

}
