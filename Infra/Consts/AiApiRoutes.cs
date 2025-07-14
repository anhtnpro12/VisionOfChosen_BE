namespace VisionOfChosen_BE.Infra.Consts
{
    public static class AiApiRoutes
    {
        public const string BaseUrl = "http://3.27.61.209:8000/";

        public static class Chat
        {
            public const string Ask = $"{BaseUrl}/api/ask";
        }
    }

}
