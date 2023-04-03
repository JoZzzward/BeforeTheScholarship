namespace BeforeTheScholarship.Context;

/// <summary>
/// Database settings that contain Type of database provider and connection string 
/// </summary>
public class DbSettings
{
    public DbType Type { get; set; }
    public string ConnectionString { get; set; }
}
