using Microsoft.Extensions.Configuration;

namespace MagicShortener.Common.Configuration
{
    public class MongoDbConfig : IMongoDbConfig
    {
        private readonly IConfiguration _config;

        public MongoDbConfig(IConfiguration config)
        {
            _config = config;
        }

        public string ConnectionString => _config.GetStringConfigParam("MongoDB:ConnectionString");
        public string DatabaseName => _config.GetStringConfigParam("MongoDB:DatabaseName");
    }
}
