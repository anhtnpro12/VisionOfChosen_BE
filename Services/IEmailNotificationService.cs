using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.AccessControl;
using VisionOfChosen_BE.DTOs.AIChat;
using VisionOfChosen_BE.DTOs.EmailNotification;
using VisionOfChosen_BE.Infra.Consts;
using VisionOfChosen_BE.Infra.Context;
using VisionOfChosen_BE.Infra.Models;
using VisionOfChosen_BE.Utils;

namespace VisionOfChosen_BE.Services
{
    public interface IEmailNotificationService
    {
        Task<List<string>> GetAllAsync(string userId);
        Task<List<EmailNotificationDto>> UpdateUserEmailsAsync(List<string> newEmails, string userId);
    }

    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly VisionOfChosen_Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpHelper _httpHelper;

        public EmailNotificationService(VisionOfChosen_Context context, IMapper mapper, IHttpHelper httpHelper)
        {
            _context = context;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }

        public async Task<List<string>> GetAllAsync(string userId)
        {
            var items = await _context.EmailNotifications
                .Where(x => x.UserId == userId)
                .Select(e => e.Email)
                .ToListAsync();

            return items;
        }

        public async Task<List<EmailNotificationDto>> UpdateUserEmailsAsync(List<string> newEmails, string userId)
        {
            // Loại bỏ email trùng trong danh sách đầu vào
            newEmails = newEmails.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            // Danh sách email hiện tại trong DB
            var existing = await _context.EmailNotifications
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var existingEmails = existing.Select(x => x.Email).ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Xác định email cần xóa
            var toDelete = existing.Where(x => !newEmails.Contains(x.Email, StringComparer.OrdinalIgnoreCase)).ToList();

            // Xác định email cần thêm
            var toAdd = newEmails
                .Where(email => !existingEmails.Contains(email))
                .Select(email => new EmailNotification
                {
                    Email = email,
                    UserId = userId,
                })
                .ToList();

            // Set email notification on AI server
            var payload = new
            {
                recipient_email = toAdd.Select(e => e.Email).FirstOrDefault(),
                resource_types = new List<string>(),
                setup_aws_config = false,
                include_global_resources = true,
                user_id = userId
            };
            var aiResponse = await _httpHelper.PostJsonAsync<object, AISetCredentialResponseDto>(
                AiApiRoutes.Chat.SetupNotifications, payload);

            _context.EmailNotifications.RemoveRange(toDelete);
            _context.EmailNotifications.AddRange(toAdd);

            await _context.SaveChangesAsync();

            var result = await _context.EmailNotifications
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<EmailNotificationDto>>(result);
        }
    }
}
