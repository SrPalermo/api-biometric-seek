using api_biometric_seek.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Request;
using models_biometric_seek.Sources.FacialAuthenticationSeek.Internal.Response;

namespace api_biometric_seek.Sources.FacialAuthenticationSeek.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class FacialAuthenticationSeekController(IServicePost<FacialAuthenticationSeekRequest, FacialAuthenticationSeekResponse> servicePost)
{
    private readonly IServicePost<FacialAuthenticationSeekRequest, FacialAuthenticationSeekResponse> _servicePost = servicePost;

    [HttpPost("FacialAuth")]
    [Produces("application/json")]
    public async Task<FacialAuthenticationSeekResponse> Post([FromForm] FacialAuthenticationSeekRequest request)
    {
        return await _servicePost.PostAsync(request);
    }
}
