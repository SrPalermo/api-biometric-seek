using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using api_biometric_seek.Common.Interfaces.Services;
using api_biometric_seek.Config.Settings;
using models.Common.DeepSeek;
using models.Sources.DeepSeekExternalApi.Request;
using models.Sources.DeepSeekExternalApi.Response;
using models.Sources.FacialAuthenticationSeek.Request;
using models.Sources.FacialAuthenticationSeek.Response;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace api_biometric_seek.Sources.FacialAuthenticationSeek.Services;

public partial class FacialAuthenticationSeekService(IServiceProvider serviceProvider) : IServicePost<FacialAuthenticationSeekRequest, FacialAuthenticationSeekResponse>
{
    private readonly Root _root = serviceProvider.GetRequiredService<Root>();

    public async Task<FacialAuthenticationSeekResponse> PostAsync(FacialAuthenticationSeekRequest data)
    {
        string imageBase64 = await ConvertFormFileToBase64(data.ImageBase);
        string imageReference64 = await ConvertFormFileToBase64(data.ImageReference);

        List<DeepSeekApiMessage> messages =
        [
            new DeepSeekApiMessage
            {
                Role = "system",
                Content = "You are an expert in biometric authentication. Your task is to create a biometric pattern from two facial images, scan them thoroughly, and determine if they match."
            },
            new DeepSeekApiMessage
            {
                Role = "user",
                Content = $"I am providing you with two facial images: " +
                          $"Image 1 (Base): {imageBase64}, " +
                          $"Image 2 (Reference): {imageReference64}. " +
                          "Your task is to perform the following steps: " +
                          "1. Create a biometric pattern for each image by analyzing key facial features, including: " +
                          "   - The distance between the eyes. " +
                          "   - The shape and size of the nose. " +
                          "   - The structure of the jawline and cheekbones. " +
                          "   - The contour of the lips and eyebrows. " +
                          "2. Compare the biometric patterns of the two images to determine if they match. " +
                          "3. Provide a confidence percentage (0-100) indicating how closely the images match. " +
                          "4. Return the result in the following JSON format: " +
                          "{\"IsMatch\": true/false, \"Confidence\": 0-100, \"Message\": \"Explanation of the result\"}. " +
                          "Do not include any additional text or explanations outside the JSON structure."
            }
        ];

        DeepSeekExternalApiRequest requestBody = new()
        {
            Model = "deepseek-chat",
            Messages = messages,
            Stream = false
        };


        JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        JsonSerializerOptions options = jsonSerializerOptions;

        string jsonBody = JsonSerializer.Serialize(requestBody, options);

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_root.ExternalProviders.DeepSeekSettings.Key}");

        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync(_root.ExternalProviders.DeepSeekSettings.Url, content);

        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();

            DeepSeekExternalApiResponse apiResponse = JsonSerializer.Deserialize<DeepSeekExternalApiResponse>(responseData)!;

            string responseContent = apiResponse.Choices[0].Message.Content;

            string jsonContent = JsonRegex().Replace(responseContent, string.Empty).Trim();

            return JsonSerializer.Deserialize<FacialAuthenticationSeekResponse>(jsonContent)!;
        }
        else
        {
            string errorMessage = await response.Content.ReadAsStringAsync();
            return new FacialAuthenticationSeekResponse
            {
                IsMatch = false,
                Confidence = 0,
                Message = $"Error en la API: {errorMessage}"
            };
        }
    }
    private static async Task<string> ConvertFormFileToBase64(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        using var image = Image.Load(memoryStream.ToArray());
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(100, 100),
            Mode = ResizeMode.Max
        }));

        using var resizedMemoryStream = new MemoryStream();
        image.Save(resizedMemoryStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder()); // Guardar como PNG
        byte[] resizedImageBytes = resizedMemoryStream.ToArray();

        return Convert.ToBase64String(resizedImageBytes);
    }

    [GeneratedRegex(@"```json|\```")]
    private static partial Regex JsonRegex();
}