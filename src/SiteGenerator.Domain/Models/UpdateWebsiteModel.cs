namespace SiteGenerator.Domain.Models;

public class UpdateWebsiteModel
{
    public string Alias { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Dictionary<string, object>? Data { get; set; }
}