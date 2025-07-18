﻿namespace VisionOfChosen_BE.Configurations
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double ExpiresInMinutes { get; set; }
    }
}
