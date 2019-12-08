namespace MagicShortener.Logic.Services
{
    /// <summary>
    /// Сервис, инкапсулирубщий операции формирования сокращенных ссылок
    /// </summary>
    public interface IUrlShorteningService
    {
        /// <summary>
        /// Метод сокращения url-a по целочисленному идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string Shorten(int id);

        /// <summary>
        /// Метод получения целочисленного идентификатора "link"-a в бд
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        int UnShorten(string shortUrl);
    }
}
