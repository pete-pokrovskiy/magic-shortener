using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MagicShortener.DataAccess.Mongo.Entities
{
    /// <summary>
    /// Пользователь API - минимально необходимый набор свойств сущности
    /// </summary>
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
