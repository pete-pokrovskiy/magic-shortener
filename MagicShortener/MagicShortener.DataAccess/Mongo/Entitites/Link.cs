﻿using MagicShortener.DataAccess.Mongo.Entitites;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MagicShortener.DataAccess.Mongo.Entities
{
    /// <summary>
    /// Ссылка для сокращения
    /// </summary>
    public class Link
    {
        [BsonId]
        public string Id { get; set; }
        public string FullLink { get; set; }
        // TODO: если сгенерированное значение может потребоваться для проведения аналитики, имеет смысл его явно хранить, а не вычислять на лету
        //public string ShortLink { get; set; }
        public DateTime? LastTimeRedirected { get; set; }
        public int RedirectsCount { get; set; }
        public User User { get; set; }
        /// <summary>
        /// Идентификатор привязки ссылок к незарегистрированным пользователям
        /// </summary>
        public string TempTokenId { get; set; }
    }
}
