using MagicShortener.Logic.Dtos;
using System.Collections.Generic;

namespace MagicShortener.Logic.Queries.Links.GetAllLinks
{
    public class GetAllLinksQueryResult : IQueryResult
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
