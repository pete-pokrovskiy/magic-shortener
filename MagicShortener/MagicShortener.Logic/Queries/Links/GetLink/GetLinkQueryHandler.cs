using MagicShortener.DataAccess;
using MagicShortener.Logic.Dtos;
using MagicShortener.Logic.Services;
using System;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Queries.Links.GetLink
{
    public class GetLinkQueryHandler : IQueryHandler<GetLinkQuery, GetLinkQueryResult>
    {
        private readonly ILinksRepository _linksRepository;
        private readonly IUrlShorteningService _urlShorteningService;

        public GetLinkQueryHandler(ILinksRepository linksRepository, 
            IUrlShorteningService urlShorteningService)
        {
            _linksRepository = linksRepository;
            _urlShorteningService = urlShorteningService;
        }
        public async Task<GetLinkQueryResult> ExecuteAsync(GetLinkQuery query)
        {
            // TODO: проверка прав доступа к текущей ссылке

            var link = await _linksRepository.Get(query.LinkId);

            if (link == null)
                return null;

            // TODO: проверка может быть вынесена либо на уровень api, либо в валидационный декоратор
            int integerId;
            bool isSuccessful = int.TryParse(query.LinkId, out integerId);
            if (!isSuccessful)
                throw new Exception($"Не удалось преобразовать идентификатор {query.LinkId} в целое число");

            return new GetLinkQueryResult   
            {
                Link = new LinkDto
                {
                    FullLink = link.FullLink,
                    ShortLink = _urlShorteningService.Shorten(integerId),
                    LastTimeRedirected = link.LastTimeRedirected,
                    RedirectsCount = link.RedirectsCount
                }
            };
        }
    }
}
