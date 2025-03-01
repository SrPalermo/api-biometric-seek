namespace models.Sources.FacialAuthenticationSeek.Response;

public class FacialAuthenticationSeekResponse
{
    public bool IsMatch { get; set; } = false;
    public double Confidence { get; set; }
    public string Message { get; set; } = null!;

}


