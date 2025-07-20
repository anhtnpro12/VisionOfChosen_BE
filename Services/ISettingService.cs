using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VisionOfChosen_BE.DTOs.EmailNotification;
using VisionOfChosen_BE.DTOs.Setting;
using VisionOfChosen_BE.Infra.Context;

namespace VisionOfChosen_BE.Services
{
    public interface ISettingService
    {
        Task<UserSettingDto> GetSettingsAsync(string userId);
    }


    public class SettingService : ISettingService
    {
        private readonly VisionOfChosen_Context _context;
        private readonly IMapper _mapper;

        public SettingService(VisionOfChosen_Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserSettingDto> GetSettingsAsync(string userId)
        {
            var awsCredential = await _context.AwsCredentials
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var emailNotifications = await _context.EmailNotifications
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Email)
                .ToListAsync();

            return new UserSettingDto
            {
                AwsCredential = awsCredential != null ? _mapper.Map<AwsCredentialDto>(awsCredential) : null,
                Emails = emailNotifications
            };
        }
    }

}
