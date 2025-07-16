namespace VisionOfChosen_BE.Infra.Consts
{
    public static class AiApiRoutes
    {
        public const string BaseUrl = "http://54.206.79.208:8000";

        public static class Chat
        {
            public const string ChatAI = $"{BaseUrl}/chat";
            public const string StartSession = $"{BaseUrl}/start-session";
        }
    }

}
