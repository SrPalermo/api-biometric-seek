namespace api_biometric_seek.Common.Interfaces.Services;

public interface IServicePost<TRequest, TResponse> where TRequest : class
{
    Task<TResponse> PostAsync(TRequest data);
}
