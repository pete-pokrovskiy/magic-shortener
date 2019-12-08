using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MagicShortener.DataAccess.Mongo.Entitites
{
    /// <summary>
    /// Вспомогательная сущность-счетчик для автоинкремента (https://docs.mongodb.com/v3.0/tutorial/create-an-auto-incrementing-field/)
    /// </summary>
    public class Counter
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

    }
}
