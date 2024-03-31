namespace SiteGenerator.Domain.Options;

public class DatabaseContextConfiguration
{
    public static string SectionName = "DatabaseContext";
    
    public string ConnectionString { get; set; }
    public string Database { get; set; }
}