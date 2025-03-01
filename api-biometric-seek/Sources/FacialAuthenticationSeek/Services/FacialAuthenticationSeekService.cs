using System.Text;
using System.Text.Json;
using api_biometric_seek.Common.Interfaces.Services;
using api_biometric_seek.Config.Settings;
using FluentValidation;
using FluentValidation.Results;
using models_biometric_seek.Common.Message;
using models_biometric_seek.Common.Parameters;
using models_biometric_seek.Sources.FacialAuthenticationSeek.External.Request;
using models_biometric_seek.Sources.FacialAuthenticationSeek.External.Response;
using models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Request;
using models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Response;
using utilities_biometric_seek.Extensions;

namespace api_biometric_seek.Sources.FacialAuthenticationSeek.Services;

public partial class FacialAuthenticationSeekService(IServiceProvider serviceProvider) : IServicePost<FacialAuthenticationSeekRequest, FacialAuthenticationSeekResponse>
{
    private readonly Root _root = serviceProvider.GetRequiredService<Root>();
    private readonly IValidator<FacialAuthenticationSeekRequest> _validatorFacialAuthenticationSeekRequest = 
        serviceProvider.GetRequiredService<IValidator<FacialAuthenticationSeekRequest>>();

    public async Task<FacialAuthenticationSeekResponse> PostAsync(FacialAuthenticationSeekRequest data)
    {
        ValidationResult validationResult = await _validatorFacialAuthenticationSeekRequest.ValidateAsync(data);

        if (!validationResult.IsValid)
        {
            throw new Exception(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
        }

        string imageBase64 = await data.ImageBase.ConvertToBase64();
        string imageReference64 = await data.ImageReference.ConvertToBase64();

        List<DeepSeekApiMessage> messages =
        [
            new DeepSeekApiMessage
            {
                Role = _root.ExternalProviders.DeepSeek.Manager.Role,
                Content = _root.ExternalProviders.DeepSeek.Manager.Content
            },
            new DeepSeekApiMessage
            {
                Role = _root.ExternalProviders.DeepSeek.Application.Role,
                Content = _root.ExternalProviders.DeepSeek.Application.Content
                    .Replace($"@{FacialAuthenticationSeekParameters.IMG_BASE}",imageBase64)
                    .Replace($"@{FacialAuthenticationSeekParameters.IMG_REF}",imageReference64)
            }
        ];

        DeepSeekExternalApiRequest requestBody = new()
        {
            Model = _root.ExternalProviders.DeepSeek.Credentials.Model,
            Messages = messages,
            Stream = false
        };

        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string jsonBody = JsonSerializer.Serialize(requestBody, options);

        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_root.ExternalProviders.DeepSeek.Credentials.Key}");

        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync(_root.ExternalProviders.DeepSeek.Credentials.Url, content);

        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();

            DeepSeekExternalApiResponse apiResponse = JsonSerializer.Deserialize<DeepSeekExternalApiResponse>(responseData)!;

            string responseContent = apiResponse.Choices[0].Message.Content.CleanJsonCodeBlock();

            return JsonSerializer.Deserialize<FacialAuthenticationSeekResponse>(responseContent)!;
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
}