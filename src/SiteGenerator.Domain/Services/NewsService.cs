using AutoMapper;
using MongoDB.Driver;
using SiteGenerator.Domain.Abstractions;
using SiteGenerator.Domain.Models;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using SiteGenerator.Domain.Entities;
using SiteGenerator.Domain.Exceptions;
using System.Text.Json;

namespace SiteGenerator.Domain.Services
{
    public class NewsService : INewsService
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;
        // private readonly IHttpContextAccessor _httpContextAccessor;

        public NewsService(IApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            // _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<NewsModel>> GetList(string alias, int pageNumber = 0,
        int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var news = await _context.News
                .AsQueryable()
                .Where(x => x.Alias == alias)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<NewsModel>>(news);
        }

        public async Task<NewsModel> CreateNews(CreateNewsModel model, CancellationToken cancellationToken)
        {
            //if (await _context.News.AsQueryable().AnyAsync(site => site.OwnerId == 1, cancellationToken: cancellationToken))
            //{
            //    throw new BusinessException("Пользователь уже является владельцем сайта");
            //}

            if (await _context.Websites.AsQueryable().AnyAsync(site => site.Alias == model.Alias, cancellationToken: cancellationToken))
            {
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
            else
                throw new BusinessException("Нет такого новостного сайта");
        }
    }
}
