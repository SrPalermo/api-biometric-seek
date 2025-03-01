using System.Net;
using api_biometric_seek.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using models.Sources.FacialAuthenticationSeek.Request;
using models.Sources.FacialAuthenticationSeek.Response;

namespace api_biometric_seek.Sources.FacialAuthenticationSeek.Controllers;

[ApiController]
[Route("[Controller]")]
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
