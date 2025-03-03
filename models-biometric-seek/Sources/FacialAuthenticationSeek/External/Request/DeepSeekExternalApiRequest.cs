using System.Text.Json.Serialization;
using models_biometric_seek.Common.Message;

namespace models_biometric_seek.Sources.FacialAuthenticationSeek.External.Request;

public class DeepSeekExternalApiRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = "deepseek-reasoner";
    [JsonPropertyName("messages")]
    public List<DeepSeekApiMessage> Messages { get; set; } = null!;
    [JsonPropertyName("stream")]
    public bool Stream { get; set; } = false;
}
