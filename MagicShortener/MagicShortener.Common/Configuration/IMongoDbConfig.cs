using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Common.Configuration
{
    /// <summary>
    /// Конфигурация MongoDb
    /// </summary>
    public interface IMongoDbConfig
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        string ConnectionString { get;}

        /// <summary>
        /// Название БД
        /// </summary>
        string DatabaseName { get; }
    }
}
