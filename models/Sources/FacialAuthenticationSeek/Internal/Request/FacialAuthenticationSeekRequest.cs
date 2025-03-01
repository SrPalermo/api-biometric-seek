using Microsoft.AspNetCore.Http;

namespace models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Request;

public class FacialAuthenticationSeekRequest
{
    public IFormFile ImageBase { get; set; } = null!;
    public IFormFile ImageReference { get; set; } = null!;
}
