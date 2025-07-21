using System.Text.Json;
using System.Text;
using VisionOfChosen_BE.DTOs.AiChatHistory;
using VisionOfChosen_BE.DTOs.AIChat;
using VisionOfChosen_BE.Utils;
using VisionOfChosen_BE.Infra.Consts;
using System.Data;
using VisionOfChosen_BE.DTOs.Setting;
using AutoMapper;
using VisionOfChosen_BE.DTOs.ScanDetail;

namespace VisionOfChosen_BE.Services
{
    public interface IAIChatService
    {
        Task<AIChatResponseDto> ProcessPromptAsync(AIChatRequestDto request, string userId, string role);
        Task<bool> SetAWSCredentials(SetAWSCredentialsRequestDto request, string userId);
        Task<bool> GenerateReportAsync(GenerateReportRequestDto request, string userId);
    }

    public class AIChatService : IAIChatService
    {
        private readonly IHttpHelper _httpHelper;
        private readonly IAIChatHistoryService _historyService;
        private readonly IAwsCredentialService _awsCredentialService;
        private readonly IScanDetailService _scanDetailService;
        private readonly IMapper _mapper;

        public AIChatService(IHttpHelper httpHelper, IAIChatHistoryService historyService, IAwsCredentialService awsCredentialService, IMapper mapper, IScanDetailService scanDetailService)
        {
            _httpHelper = httpHelper;
            _historyService = historyService;
            _awsCredentialService = awsCredentialService;
            _mapper = mapper;
            _scanDetailService = scanDetailService;
        }

        public async Task<AIChatResponseDto> ProcessPromptAsync(AIChatRequestDto request, string userId, string role)
        {
            // 1. Lưu câu hỏi người dùng
            var userMsg = new AiChatHistoryCreateDto
            {
                SessionId = request.SessionId,
                Message = request.Message,
                Timestamp = DateTime.Now,
                Files = request.Files,
            };
            await _historyService.CreateAsync(userMsg, userId, role);

            // 2. Gửi request lên AI
            var payload = new
            {
                message = request.Message,
                session_id = request.SessionId
            };
            var headers = new Dictionary<string, string>
            {
                { "X-Session-ID", request.SessionId }
            };
            var aiResponse = await _httpHelper.PostJsonAsync<object, AIResponseDto>(
                AiApiRoutes.Chat.ChatAI, payload, headers);

            // 4. Lưu phản hồi của AI
            var aiMsg = new AiChatHistoryCreateDto
            {
                SessionId = request.SessionId,
                Message = aiResponse.Response ?? "Không có phản hồi",
                Timestamp = DateTime.Now
            };
            await _historyService.CreateAsync(aiMsg, userId, RoleConst.AI);

            // 5. Trả kết quả
            return new AIChatResponseDto
            {
                Role = RoleConst.AI,
                Message = aiResponse.Response ?? "Không có phản hồi"
            };
        }

        public async Task<bool> SetAWSCredentials(SetAWSCredentialsRequestDto request, string userId)
        {
            // 1. Lưu credential
            var awsCredentialRequest = _mapper.Map<AwsCredentialCreateDto>(request);
            await _awsCredentialService.CreateOrUpdateAsync(awsCredentialRequest, userId);

            // 2. Set Credential trên AI server
            var payload = new
            {
                aws_access_key_id = request.AwsAccessKeyId,
                aws_secret_access_key = request.AwsSecretAccessKey,
                aws_region = request.AwsRegion,
                user_id = userId
            };
            var headers = new Dictionary<string, string>
            {
                { "X-Session-ID", request.SessionId }
            };
            var aiResponse = await _httpHelper.PostJsonAsync<object, AISetCredentialResponseDto>(
                AiApiRoutes.Chat.SetAWSCredentials, payload, headers);

            return true;
        }

        public async Task<bool> GenerateReportAsync(GenerateReportRequestDto request, string userId)
        {
            var payload = new
            {
                session_id = request.SessionId,
            };
            var headers = new Dictionary<string, string>
            {
                { "X-Session-ID", request.SessionId }
            };
            var aiResponse = await _httpHelper.GetJsonAsync<List<ScanDetailDto>>(
                AiApiRoutes.Chat.Report, headers);

            await _scanDetailService.CreateManyAsync(aiResponse, userId);

            return true;
        }
    }

}
