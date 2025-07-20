using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using VisionOfChosen_BE.DTOs.AIChat;
using VisionOfChosen_BE.DTOs.AiChatHistory;
using VisionOfChosen_BE.DTOs.FileStorage;
using VisionOfChosen_BE.Infra.Consts;
using VisionOfChosen_BE.Infra.Context;
using VisionOfChosen_BE.Infra.Models;
using VisionOfChosen_BE.Utils;

namespace VisionOfChosen_BE.Services
{
    public interface IAIChatHistoryService
    {
        Task<List<AiChatHistoryDto>> GetAllAsync(string sessionId, string userId);
        Task<AiChatHistoryDto?> GetByIdAsync(string id, string userId);
        Task<AiChatHistoryDto> CreateAsync(AiChatHistoryCreateDto dto, string userId, string role);
        Task<AiChatHistoryDto?> UpdateAsync(string id, AiChatHistoryUpdateDto dto, string actorId);
        Task<bool> DeleteAsync(string id, string actorId);
        Task<List<ChatSessionDto>> GetChatSessionsAsync(string userId);
        Task<NewSessionDto> GetNewChatSessionAsync();
    }
    public class AIChatHistoryService : IAIChatHistoryService
    {
        private readonly VisionOfChosen_Context _context;
        private readonly IHttpHelper _httpHelper;
        private readonly IMapper _mapper;

        public AIChatHistoryService(VisionOfChosen_Context context, IHttpHelper httpHelper, IMapper mapper)
        {
            _context = context;
            _httpHelper = httpHelper;
            _mapper = mapper;
        }

        public async Task<List<AiChatHistoryDto>> GetAllAsync(string sessionId, string userId)
        {
            return await _context.AiChatHistories
                .Where(x => !x.deleted && x.SessionId == sessionId && x.UserId == userId)
                .OrderBy(x => x.Timestamp)
                .Select(e => _mapper.Map<AiChatHistoryDto>(e))
                .ToListAsync();
        }

        public async Task<AiChatHistoryDto?> GetByIdAsync(string id, string userId)
        {
            var entity = await _context.AiChatHistories
                .FirstOrDefaultAsync(x => x.id == id && !x.deleted && x.UserId == userId);

            return entity == null ? null : _mapper.Map<AiChatHistoryDto>(entity);
        }

        public async Task<AiChatHistoryDto> CreateAsync(AiChatHistoryCreateDto dto, string userId, string role)
        {
            var entity = new AiChatHistory
            {
                SessionId = dto.SessionId,
                UserId = userId,
                Role = role,
                Message = dto.Message,
                Timestamp = dto.Timestamp,
                Files = dto.Files.Select(e => new FileBase
                {
                    OriginalFileName = e.OriginalFileName,
                    SavedFileName = e.SavedFileName,
                    Size = e.Size,
                    ContentType = e.ContentType,
                    Url = e.Url
                }).ToList()
            }.Created(userId);

            _context.AiChatHistories.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<AiChatHistoryDto>(entity);
        }

        public async Task<AiChatHistoryDto?> UpdateAsync(string id, AiChatHistoryUpdateDto dto, string actorId)
        {
            var entity = await _context.AiChatHistories.FirstOrDefaultAsync(x => x.id == id && !x.deleted && x.UserId == actorId);
            if (entity == null) return null;

            entity.SessionId = dto.SessionId;
            entity.Role = dto.Role;
            entity.Message = dto.Message;
            entity.Timestamp = dto.Timestamp;
            entity.Modified(actorId);

            _context.AiChatHistories.Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<AiChatHistoryDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id, string actorId)
        {
            var entity = await _context.AiChatHistories.FirstOrDefaultAsync(x => x.id == id && !x.deleted && x.UserId == actorId);
            if (entity == null) return false;

            entity.Deleted(actorId);
            _context.AiChatHistories.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ChatSessionDto>> GetChatSessionsAsync(string userId)
        {
            var sessions = _context.AiChatHistories
                .Where(ch => userId == ch.UserId && !ch.deleted)
                .AsEnumerable()
                .GroupBy(ch => ch.SessionId)
                .Select(g => 
                {
                    var firstMessage = g.OrderBy(ch => ch.Timestamp).Select(ch => ch.Message).FirstOrDefault();
                    var title = $"Session {g.Key}";
                    var preview = string.Empty;
                    if (firstMessage != null)
                    {
                        title = firstMessage.Substring(0, Math.Min(firstMessage.Length, 30));
                        preview = firstMessage.Substring(0, Math.Min(firstMessage.Length, 50));
                    }

                    return new ChatSessionDto
                    {
                        SessionId = g.Key,
                        Title = title,
                        CreatedAt = g.Min(ch => ch.Timestamp),
                        LastActivity = g.Max(ch => ch.Timestamp),
                        Preview = preview,
                        MessageCount = g.Count()
                    };
                })
                .OrderByDescending(dto => dto.LastActivity)
                .ToList();

            return sessions;
        }

        public async Task<NewSessionDto> GetNewChatSessionAsync()
        {
            var response = await _httpHelper.PostJsonAsync<object, NewSessionDto>(
                AiApiRoutes.Chat.StartSession, new {});

            return response;
        }
    }
}
