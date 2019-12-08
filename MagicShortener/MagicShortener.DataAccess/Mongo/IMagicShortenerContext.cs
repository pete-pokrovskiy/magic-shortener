using MagicShortener.DataAccess.Mongo.Entities;
using MagicShortener.DataAccess.Mongo.Entitites;
using MongoDB.Driver;

namespace MagicShortener.DataAccess.Mongo
{
    public interface IMagicShortenerContext
    {
        IMongoCollection<Link> Links { get; }
        IMongoCollection<Counter> Counters { get; }
        IMongoCollection<User> Users { get; }
    }
}