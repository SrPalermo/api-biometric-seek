using utilities_biometric_seek.Source.FacialAuthenticationSeek;

namespace utilities_biometric_seek.Extensions;

public static class StringExtensions
{
    public static string CleanJsonCodeBlock(this string input)
    {
        return DeepSeekRegexUtility.JsonRegex().Replace(input, string.Empty).Trim();
    }
}
