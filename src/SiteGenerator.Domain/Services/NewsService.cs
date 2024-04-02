using AutoMapper;
using MongoDB.Driver;
using SiteGenerator.Domain.Abstractions;
using SiteGenerator.Domain.Models;
using MongoDB.Driver.Linq;
using SiteGenerator.Domain.Entities;
using SiteGenerator.Domain.Exceptions;

namespace SiteGenerator.Domain.Services
{
    public class NewsService : INewsService
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public NewsService(IApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NewsModel>> GetList(string alias, int pageNumber = 0,
        int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var news = await _context.News
                .AsQueryable()
                .Where(x => x.Alias == alias)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .OrderByDescending(site => site.Created)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<NewsModel>>(news);
        }

        public async Task<NewsModel> CreateNews(CreateNewsModel model, CancellationToken cancellationToken)
        {
            if (!await _context.Websites.AsQueryable().AnyAsync(site => site.Alias == model.Alias, cancellationToken: cancellationToken))
                throw new BusinessException("Нет такого новостного сайта");
            
            var news = new News
            {
                Alias = model.Alias,
                Title = model.Title,
                Image = model.Image,
                Description = model.Description,
                Created = DateTime.Now,
                // OwnerId = _httpContextAccessor.HttpContext.User
            };

            await _context.News.InsertOneAsync(news, cancellationToken: cancellationToken);

            return _mapper.Map<NewsModel>(news);
        }

        public async Task DeleteNewsByAliasAndId(string alias, string id, CancellationToken cancellationToken)
        {
            var news = await _context.News.AsQueryable()
                .Where(x => x.Alias == alias && x.Id.ToString() == id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (news == null)
                throw new EntityNotFoundException("Не удалось найти новость с таким id");

            // if (website.OwnerId != 1)
            //     throw new BusinessException("Вы не владелец вебсайта");

            await _context.News.DeleteOneAsync(x => x.Id.ToString() == id, cancellationToken);
        }
    }
}
