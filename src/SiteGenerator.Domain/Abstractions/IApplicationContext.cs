using MongoDB.Driver;
using SiteGenerator.Domain.Entities;

namespace SiteGenerator.Domain.Abstractions;

public interface IApplicationContext
{
    IMongoCollection<Website> Websites { get; }
}