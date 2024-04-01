namespace SiteGenerator.Web.Contracts.Requests
{
    public class CreateNewsRequest
    {
        public string Alias { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
