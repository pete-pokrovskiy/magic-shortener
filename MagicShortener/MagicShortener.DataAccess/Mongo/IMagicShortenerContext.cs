using MagicShortener.DataAccess.Mongo.Entities;
using MongoDB.Driver;

namespace MagicShortener.DataAccess.Mongo
{
    public interface IMagicShortenerContext
    {
        IMongoCollection<Link> Links { get; }
    }
}