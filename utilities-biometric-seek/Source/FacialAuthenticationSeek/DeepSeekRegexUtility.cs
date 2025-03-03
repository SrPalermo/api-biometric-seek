using System.Text.RegularExpressions;

namespace utilities_biometric_seek.Source.FacialAuthenticationSeek;

public static partial class DeepSeekRegexUtility
{
    [GeneratedRegex(@"```json|\```")]
    public static partial Regex JsonRegex();
}
