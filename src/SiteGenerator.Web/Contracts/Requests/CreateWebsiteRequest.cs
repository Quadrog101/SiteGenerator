namespace SiteGenerator.Web.Contracts.Requests;

public class CreateWebsiteRequest
{
    public string Alias { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    
    public Dictionary<string, object> Data { get; set; }
}