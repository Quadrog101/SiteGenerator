using MongoDB.Bson;
using SiteGenerator.Domain.Models;

namespace SiteGenerator.Domain.Abstractions;

public interface IWebsiteService
{
    Task<IEnumerable<WebsiteModel>> GetList(int pageNumber = 0,
        int pageSize = 5, CancellationToken cancellationToken = default);

    Task<string> CreateNewSite(CreateWebsiteModel model, CancellationToken cancellationToken);

    Task<WebsiteModel> GetWebsiteByAlias(string alias, CancellationToken cancellationToken);

    Task<bool> UpdateWebsiteData(UpdateWebsiteModel model, CancellationToken cancellationToken);

    Task DeleteWebsiteByAlias(string alias, CancellationToken cancellationToken);
}