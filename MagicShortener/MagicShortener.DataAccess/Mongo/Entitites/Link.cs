using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MagicShortener.DataAccess.Mongo.Entities
{
    /// <summary>
    /// Основаня сущность - ссылка
    /// </summary>
    public class Link
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FullLink { get; set; }
        public string ShortLink { get; set; }
        public DateTime? LastTimeAccessed { get; set; }
        public int RedirectsCount { get; set; }
    }
}
