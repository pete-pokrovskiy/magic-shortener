using MagicShortener.Logic.Dtos;

namespace MagicShortener.Logic.Queries.Links.GetLink
{
    public class GetLinkQueryResult : IQueryResult
    {
        public LinkDto Link { get; internal set; }
    }
}
