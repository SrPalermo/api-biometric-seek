using api_biometric_seek.Common.Interfaces.Services;
using api_biometric_seek.Sources.FacialAuthenticationSeek.Services;
using models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Request;
using models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Response;

namespace api_biometric_seek.Config.DependencyInjections;
public static class ServiceInjections
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        #region FACIAL AUTHENTICATION SEEK

        services.AddScoped<IServicePost<FacialAuthenticationSeekRequest, FacialAuthenticationSeekResponse>, FacialAuthenticationSeekService>();
        
        #endregion

        return services;
    }
}
