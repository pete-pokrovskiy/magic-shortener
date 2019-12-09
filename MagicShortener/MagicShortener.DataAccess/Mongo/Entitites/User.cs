using MagicShortener.DataAccess.Mongo.Entitites;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MagicShortener.DataAccess.Mongo.Entities
{
    /// <summary>
    /// Пользователь API - минимально необходимый набор свойств
    /// </summary>
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
