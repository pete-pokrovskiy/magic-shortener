namespace MagicShortener.Logic.Queries.Links.GetLink
{
    /// <summary>
    /// Получение ссылки по идентификатору
    /// TODO: более гибким будет вариант с передачей фильтра по нескольким полям
    /// </summary>
    public class GetLinkQuery : IQuery
    {
        public string LinkId { get; set; }
    }
}
