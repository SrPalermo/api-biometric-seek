using FluentValidation;
using Microsoft.AspNetCore.Http;
using models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Request;

namespace validations_biometric_seek.Source.FacialAuthenticationSeek.Internal.Request;

public class FacialAuthenticationSeekRequestValidator : AbstractValidator<FacialAuthenticationSeekRequest>, IValidator<FacialAuthenticationSeekRequest>
{
    private readonly List<string> _allowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff"];
    private const long MaxFileSizeInBytes = 1024 * 1024; 

    public FacialAuthenticationSeekRequestValidator()
    {
        ExecuteRules();
    }
    private void ExecuteRules()
    {
        RuleFor(x => x.ImageBase)
            .NotNull().WithMessage("La imagen base es requerida.")
            .Must(BeAnImage).WithMessage("La imagen base debe ser un archivo de imagen válido.")
            .Must(BeWithinSizeLimit).WithMessage("La imagen base no debe exceder 1 MB.");

        RuleFor(x => x.ImageReference)
            .NotNull().WithMessage("La imagen de referencia es requerida.")
            .Must(BeAnImage).WithMessage("La imagen de referencia debe ser un archivo de imagen válido.")
            .Must(BeWithinSizeLimit).WithMessage("La imagen de referencia no debe exceder 1 MB.");
    }

    private bool BeAnImage(IFormFile file)
    {
        if (file == null)
            return false;

        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        return _allowedExtensions.Contains(fileExtension);
    }
    private bool BeWithinSizeLimit(IFormFile file)
    {
        if (file == null)
            return false;

        return file.Length <= MaxFileSizeInBytes;
    }
}