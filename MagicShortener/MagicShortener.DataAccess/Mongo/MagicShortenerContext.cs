using MagicShortener.Common.Configuration;
using MagicShortener.DataAccess.Mongo.Entities;
using MagicShortener.DataAccess.Mongo.Entitites;
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

            SeedWithInitialData();
        }
        public IMongoCollection<Link> Links => _mongoDb.GetCollection<Link>("Links");

        public IMongoCollection<Counter> Counters => _mongoDb.GetCollection<Counter>("Counters");

        public IMongoCollection<User> Users => _mongoDb.GetCollection<User>("Users");

        /// <summary>
        /// Метод сидирования данных
        /// </summary>
        private void SeedWithInitialData()
        {
            // если отсутствует счетчик по ссылкам, то создадим запись и проинициализируем первым значением
            if(!Counters.Find(Builders<Counter>.Filter.Eq(m => m.Name, Constants.LinkIdCounterName)).Any())
            {
                Counters.InsertOne(new Counter { Name = Constants.LinkIdCounterName, Value = 1 });
            }     
        }
    }
}
