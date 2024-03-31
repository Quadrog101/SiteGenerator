using System.Text.Json;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SiteGenerator.Domain.Abstractions;
using SiteGenerator.Domain.Entities;
using SiteGenerator.Domain.Exceptions;
using SiteGenerator.Domain.Models;

namespace SiteGenerator.Domain.Services;

public class WebsiteService : IWebsiteService
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    // private readonly IHttpContextAccessor _httpContextAccessor;

    public WebsiteService(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        // _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<WebsiteModel>> GetList(int pageNumber = 0, 
        int pageSize = 5, CancellationToken cancellationToken = default)
    {
        var sites = await _context.Websites
            .AsQueryable()
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<WebsiteModel>>(sites);
    }

    public async Task<string> CreateNewSite(CreateWebsiteModel model, CancellationToken cancellationToken)
    {
        if (await _context.Websites.AsQueryable().AnyAsync(site => site.OwnerId == 1, cancellationToken: cancellationToken))
        {
            throw new BusinessException("Пользователь уже является владельцем сайта");
        }

        if (await _context.Websites.AsQueryable().AnyAsync(site => site.Alias == model.Alias, cancellationToken: cancellationToken))
        {
            throw new BusinessException("Доменное имя занято");
        }

        var serializedData = JsonSerializer.Serialize(model.Data);
        var website = new Website
        {
            Name = model.Name,
            Type = model.Type,
            Alias = model.Alias,
            // OwnerId = _httpContextAccessor.HttpContext.User
            OwnerId = 1,
            Data = BsonDocument.Parse(serializedData)
        };
        
        await _context.Websites.InsertOneAsync(website, cancellationToken: cancellationToken);

        return website.Id.ToString();
    }

    public async Task<WebsiteModel> GetWebsiteByAlias(string alias, CancellationToken cancellationToken)
    {
        var website = await _context.Websites.AsQueryable()
            .Where(x => x.Alias == alias)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (website == null)
            throw new EntityNotFoundException("Не удалось найти вебсайт с таким именем");

        return _mapper.Map<WebsiteModel>(website);
    }

    public async Task<bool> UpdateWebsiteData(UpdateWebsiteModel model, CancellationToken cancellationToken)
    {
        var website = await _context.Websites.AsQueryable()
            .Where(x => x.Alias == model.Alias)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (website == null)
            throw new EntityNotFoundException("Не удалось найти вебсайт с таким именем");

        // if (website.OwnerId != 1)
        //     throw new BusinessException("Вы не владелец вебсайта");
        
        var update = Builders<Website>.Update
            .Set(site => site.Name, model.Name)
            .Set(site => site.Type, model.Type);
        if (model.Data != null)
            update = update.Set(site => site.Data, BsonDocument.Parse(JsonSerializer.Serialize(model.Data)));

        var result = await _context.Websites.UpdateOneAsync(site => site.Alias == model.Alias, update, 
            cancellationToken: cancellationToken);

        return result.IsAcknowledged;
    }

    public async Task DeleteWebsiteByAlias(string alias, CancellationToken cancellationToken)
    {
        var website = await _context.Websites.AsQueryable()
            .Where(x => x.Alias == alias)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (website == null)
            throw new EntityNotFoundException("Не удалось найти вебсайт с таким именем");
        
        // if (website.OwnerId != 1)
        //     throw new BusinessException("Вы не владелец вебсайта");

        await _context.Websites.DeleteOneAsync(x => x.Alias == alias, cancellationToken);
    }
}