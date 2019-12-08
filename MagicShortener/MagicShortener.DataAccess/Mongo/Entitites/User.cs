using MagicShortener.DataAccess.Mongo.Entitites;

namespace MagicShortener.DataAccess.Mongo.Entities
{
    /// <summary>
    /// Пользователь API - минимально необходимый набор свойств сущности
    /// </summary>
    public class User
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
