using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Dtos
{
    public class LinkDto
    {
        public string FullLink { get; set; }
        public string ShortLink { get; set; }
        public DateTime? LastTimeRedirected { get; set; }
        public int RedirectsCount { get; set; }

    }
}
