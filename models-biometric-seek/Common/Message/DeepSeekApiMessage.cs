using System.Text.Json.Serialization;

namespace models_biometric_seek.Common.Message;

public class DeepSeekApiMessage
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = null!;
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
