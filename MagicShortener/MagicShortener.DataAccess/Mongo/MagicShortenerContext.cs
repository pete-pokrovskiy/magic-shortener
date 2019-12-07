using MagicShortener.Common.Configuration;
using MagicShortener.DataAccess.Mongo.Entities;
using MongoDB.Driver;

namespace MagicShortener.DataAccess.Mongo
{
    /// <summary>
    /// Контекст для работы с MongoDB
    /// </summary>
    public class MagicShortenerContext : IMagicShortenerContext
    {
        private readonly IMongoDatabase _mongoDb;
        public MagicShortenerContext(IMongoDbConfig config)
        {
            _mongoDb = new MongoClient(config.ConnectionString).GetDatabase(config.DatabaseName);
        }
        public IMongoCollection<Link> Links => _mongoDb.GetCollection<Link>("sLinks");
    }
}
