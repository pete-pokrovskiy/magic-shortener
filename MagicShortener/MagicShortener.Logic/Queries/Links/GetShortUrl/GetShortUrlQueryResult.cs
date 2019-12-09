using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Queries.Links.ConvertShortUrlToFull
{
    public class GetShortUrlQueryResult : IQueryResult
    {
        public string LinkId { get; set; }
        public string FullUrl { get; set; }
    }
}
