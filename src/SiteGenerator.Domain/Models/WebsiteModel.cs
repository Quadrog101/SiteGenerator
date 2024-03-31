namespace SiteGenerator.Domain.Models;

public class WebsiteModel
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Alias { get; set; }
    public Dictionary<string, object> Data { get; set; }
}