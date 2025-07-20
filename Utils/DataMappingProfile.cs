using AutoMapper;
using VisionOfChosen_BE.DTOs.AIChat;
using VisionOfChosen_BE.DTOs.AiChatHistory;
using VisionOfChosen_BE.DTOs.EmailNotification;
using VisionOfChosen_BE.DTOs.FileStorage;
using VisionOfChosen_BE.DTOs.Setting;
using VisionOfChosen_BE.Infra.Models;

namespace VisionOfChosen_BE.Utils
{
    public class AIChatProfile : Profile
    {
        public AIChatProfile()
        {
            CreateMap<FileBase, FileUploadResult>().ReverseMap();
            CreateMap<AiChatHistory, AiChatHistoryDto>().ReverseMap();
            CreateMap<AwsCredentialCreateDto, SetAWSCredentialsRequestDto>().ReverseMap();
        }
    }

    public class AwsCredentialProfile : Profile
    {
        public AwsCredentialProfile()
        {
            CreateMap<AwsCredential, AwsCredentialDto>()
                .ForMember(dest => dest.PemFile, opt => opt.MapFrom(src => src.PemFile));

            CreateMap<AwsCredentialCreateDto, AwsCredential>()
                .ForMember(dest => dest.PemFile, opt => opt.MapFrom(src => src.PemFile));

            CreateMap<AwsCredentialUpdateDto, AwsCredential>()
                .ForMember(dest => dest.PemFile, opt => opt.MapFrom(src => src.PemFile));
        }
    }

    public class EmailNotificationProfile : Profile
    {
        public EmailNotificationProfile()
        {
            CreateMap<EmailNotification, EmailNotificationDto>();
            CreateMap<EmailNotificationCreateDto, EmailNotification>();
            CreateMap<EmailNotificationUpdateDto, EmailNotification>();
        }
    }
}
