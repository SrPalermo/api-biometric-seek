using System.Text.Json.Serialization;
using models.Common.DeepSeek;

namespace models.Sources.DeepSeekExternalApi.Request;

public class DeepSeekExternalApiRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = "deepseek-reasoner";
    [JsonPropertyName("messages")]
    public List<DeepSeekApiMessage> Messages { get; set; } = null!;
    [JsonPropertyName("stream")]
    public bool Stream { get; set; } = false;
}
