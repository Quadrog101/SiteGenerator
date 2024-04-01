using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SiteGenerator.Domain.Abstractions;
using SiteGenerator.Domain.Entities;
using SiteGenerator.Domain.Options;

namespace SiteGenerator.Infrastructure.Db;

public class ApplicationContext : IApplicationContext
{
    private readonly IMongoDatabase _database;
    
    public IMongoCollection<Website> Websites => _database.GetCollection<Website>(nameof(Websites));
    public IMongoCollection<News> News => _database.GetCollection<News>(nameof(News));

    public ApplicationContext(IOptions<DatabaseContextConfiguration> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.Database);
    }
}