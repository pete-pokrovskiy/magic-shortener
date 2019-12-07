using System;

namespace MagicShortener.Domain
{
    /// <summary>
    /// Основаня сущность - ссылка
    /// </summary>
    public class Link
    {
        public string FullLink { get; set; }
        public string ShortLink { get; set; }
        public DateTime? LastTimeAccessed { get; set; }
        public int RedirectsCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
    }
}
