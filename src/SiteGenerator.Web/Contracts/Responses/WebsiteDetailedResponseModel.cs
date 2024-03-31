namespace SiteGenerator.Web.Contracts.Responses;

public class WebsiteDetailedResponseModel
{
    public string Alias { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }
    
    public Dictionary<string, object> Data { get; set; }
}