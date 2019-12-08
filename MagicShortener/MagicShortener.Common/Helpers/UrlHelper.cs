using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Common.Helpers
{
    /// <summary>
    /// Класс-помощник для работы с url-ом
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        /// Метод проверки достпности URL
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
        public async static Task<bool> IsUrlReachable(string urlString)
        {
            try
            {
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Head, new Uri(urlString)))
                {

                    using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        return response.IsSuccessStatusCode;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

