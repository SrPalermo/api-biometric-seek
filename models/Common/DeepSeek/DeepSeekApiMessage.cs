using System.Text.Json.Serialization;

namespace models.Common.DeepSeek;

public class DeepSeekApiMessage
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = null!;
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
