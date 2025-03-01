using Microsoft.AspNetCore.Http;

namespace models.Sources.FacialAuthenticationSeek.Request;

public class FacialAuthenticationSeekRequest
{
    public IFormFile ImageBase { get; set; } = null!;
    public IFormFile ImageReference { get; set; } = null!;
}
