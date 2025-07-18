using VisionOfChosen_BE.Services;
using VisionOfChosen_BE.Utils;

namespace VisionOfChosen_BE
{
    public static class ServiceInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IAIChatHistoryService, AIChatHistoryService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IScanService, ScanService>();
            services.AddScoped<IAIChatService, AIChatService>();
            services.AddScoped<IScanDetailService, ScanDetailService>();
            services.AddHttpClient<IHttpHelper, HttpHelper>(client =>
            {
                client.Timeout = TimeSpan.FromMinutes(10);
            });
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
