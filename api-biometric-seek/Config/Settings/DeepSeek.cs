namespace api_biometric_seek.Config.Settings;

public class DeepSeek
{
    public Credentials Credentials { get; set; } = null!;
    public Manager Manager { get; set; } = null!;
    public Application Application { get; set; } = null!;
}
public class Credentials
{
    public string Key { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Model { get; set; } = null!;
}
public class Manager : Parameters { }
public class Application : Parameters { }
public class Parameters
{
    public string Role { get; set; } = null!;
    public string Content { get; set; } = null!;
}
