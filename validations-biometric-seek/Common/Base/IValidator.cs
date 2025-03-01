using FluentValidation.Results;

namespace validations_biometric_seek.Common.Base;
public interface IValidator<T> where T : class
{
    Task<ValidationResult> ValidateAsync(T request);
}