namespace BeforeTheScholarship.Services.Settings;

public class CacheSettings
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string Password { get; set; }
    public TimeSpan CacheLifetime { get; set; }
}
