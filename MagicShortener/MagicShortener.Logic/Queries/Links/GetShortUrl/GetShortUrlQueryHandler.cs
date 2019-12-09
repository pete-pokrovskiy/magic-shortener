using MagicShortener.DataAccess;
using MagicShortener.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Queries.Links.ConvertShortUrlToFull
{
    public class GetShortUrlQueryHandler : IQueryHandler<GetShortUrlQuery, GetShortUrlQueryResult>
    {
        private readonly ILinksRepository _linksRepository;
        private readonly IUrlShorteningService _urlShorteningService;

        public GetShortUrlQueryHandler(ILinksRepository linksRepository,
            IUrlShorteningService urlShorteningService)
        {
            _linksRepository = linksRepository;
            _urlShorteningService = urlShorteningService;
        }

        public async Task<GetShortUrlQueryResult> ExecuteAsync(GetShortUrlQuery query)
        {
            var linkId = _urlShorteningService.UnShorten(query.ShortUrl);
            var link = await _linksRepository.Get(linkId.ToString());

            if (link == null)
                return null;

            return new GetShortUrlQueryResult
            {
                LinkId = link.Id,
                FullUrl = link.FullLink
            };
        }
    }
}
