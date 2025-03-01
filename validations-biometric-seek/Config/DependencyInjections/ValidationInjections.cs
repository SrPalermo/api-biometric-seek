using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Request;
using validations_biometric_seek.Source.FacialAuthenticationSeek.Internal.Request;

namespace validations_biometric_seek.Config.DependencyInjections;

public static class ValidationInjections
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();


        #region FACIAL AUTHENTICATION SEEK

        services.AddScoped<IValidator<FacialAuthenticationSeekRequest>, FacialAuthenticationSeekRequestValidator>();

        #endregion

        return services;

    }
}
