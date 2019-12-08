using MagicShortener.DataAccess;
using MagicShortener.Logic.Dtos;
using MagicShortener.Logic.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Queries.Links.GetAllLinks
{
    public class GetAllLinksQueryHandler : IQueryHandler<GetAllLinksQuery, GetAllLinksQueryResult>
    {
        private readonly ILinksRepository _linksRepository;
        private readonly IUrlShorteningService _urlShorteningService;

        public GetAllLinksQueryHandler(ILinksRepository linksRepository,
            IUrlShorteningService urlShorteningService)
        {
            _linksRepository = linksRepository;
            _urlShorteningService = urlShorteningService;
        }
        public async Task<GetAllLinksQueryResult> ExecuteAsync(GetAllLinksQuery query)
        {
            return new GetAllLinksQueryResult
            {
                Links = (await _linksRepository.GetAll()).Select(l => new LinkDto
                {
                    FullLink = l.FullLink,
                    ShortLink = _urlShorteningService.Shorten(int.Parse(l.Id)),
                    LastTimeRedirected = l.LastTimeRedirected,
                    RedirectsCount = l.RedirectsCount
                }).ToList()
            };
        }
    }
}
