using SiteGenerator.Domain.Models;

namespace SiteGenerator.Domain.Abstractions
{
    public interface INewsService
    {
        Task<IEnumerable<NewsModel>> GetList(string alias, int pageNumber = 0,
            int pageSize = 5, CancellationToken cancellationToken = default);

        Task<NewsModel> CreateNews(CreateNewsModel model, CancellationToken cancellationToken);
    }
}
