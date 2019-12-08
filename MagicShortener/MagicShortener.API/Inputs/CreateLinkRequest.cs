namespace MagicShortener.API.Inputs
{
    /// <summary>
    /// Данные запроса на создание ссылки
    /// </summary>
    public class CreateLinkRequest
    {
        public string Url { get; set; }
    }
}
