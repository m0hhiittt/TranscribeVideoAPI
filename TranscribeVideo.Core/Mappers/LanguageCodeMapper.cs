using System;
using System.Collections.Generic;

namespace TranscribeVideo.Infrastructure.Services
{
    public static class LanguageCodeMapper
    {
        private static readonly Dictionary<string, string> Map = new(StringComparer.OrdinalIgnoreCase)
        {
            ["english"] = "en",
            ["spanish"] = "es",
            ["french"] = "fr",
            ["german"] = "de",
            ["hindi"] = "hi",
            ["chinese"] = "zh",
            ["japanese"] = "ja",
            ["korean"] = "ko",
            ["portuguese"] = "pt",
            ["italian"] = "it",
            ["russian"] = "ru",
            ["arabic"] = "ar"
        };

       public static string? ToIsoCode(string? language)
        {
            if (string.IsNullOrWhiteSpace(language))
                return null;

            var trimmed = language.Trim();

            if (Map.TryGetValue(trimmed, out var code))
                return code;

            return trimmed.Length == 2 ? trimmed.ToLowerInvariant() : null;
        }
    }
}
