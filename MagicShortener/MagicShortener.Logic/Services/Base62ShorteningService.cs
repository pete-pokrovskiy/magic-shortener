using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Services
{
    /// <summary>
    /// Сервис сокращения ссылок на основе перевода десятиричного идентификатора в 62-ричный
    /// </summary>
    public class Base62ShorteningService : IUrlShorteningService
    {

        /// <summary>
        /// Входящая последовательность, по которой формируем короткую ссылку - используем английский алфавит в нижнем и верхнем регистре + 10 цифр
        /// </summary>
        private static readonly string InputCharacterSequence = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string Shorten(int id)
        {
            if (id == 0)
                return InputCharacterSequence[0].ToString();

            StringBuilder shortUrlSb = new StringBuilder();
 
            while (id > 0)
            {
                shortUrlSb.Append(InputCharacterSequence[id % InputCharacterSequence.Length]);
                id = id / InputCharacterSequence.Length;
            }
            return string.Join(string.Empty, shortUrlSb.ToString().Reverse());
        }

        public int UnShorten(string shortUrl)
        {
            var id = 0;

            foreach (var character in shortUrl)
                id = (id * InputCharacterSequence.Length) + InputCharacterSequence.IndexOf(character);

            return id;
        }
    }
}
