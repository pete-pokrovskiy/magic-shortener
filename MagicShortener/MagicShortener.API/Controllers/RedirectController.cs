using MagicShortener.Logic.Commands;
using MagicShortener.Logic.Commands.Links.IncrementLinkRedirectsCount;
using MagicShortener.Logic.Queries;
using MagicShortener.Logic.Queries.Links.ConvertShortUrlToFull;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MagicShortener.API.Controllers
{
    /// <summary>
    /// Контроллер для редиректа - используем короткий раут
    /// </summary>
    [Route("r")]
    public class RedirectController : Controller
    {
        private readonly ICommandHandler<IncrementLinkRedirectsCountCommand> _incrementLinkRedirectsCountCommandHandler;
        private readonly IQueryHandler<GetShortUrlQuery, GetShortUrlQueryResult> _convertShortUrlToFullQueryHandler;

        public RedirectController(
            ICommandHandler<IncrementLinkRedirectsCountCommand> incrementLinkRedirectsCountCommandHandler,
            IQueryHandler<GetShortUrlQuery, GetShortUrlQueryResult> convertShortUrlToFullQueryHandler)
        {
            _incrementLinkRedirectsCountCommandHandler = incrementLinkRedirectsCountCommandHandler;
            _convertShortUrlToFullQueryHandler = convertShortUrlToFullQueryHandler;
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectToFullUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                return BadRequest(Constants.NoShortUrlError);

            
            var result = await _convertShortUrlToFullQueryHandler.ExecuteAsync(new GetShortUrlQuery
            {
                ShortUrl = shortUrl
            });

            // TODO: в зависимости от трмебований возращаемый код может быть иным
            if (result == null)
                return NotFound();

            //инкремент счетчика переходов
            await _incrementLinkRedirectsCountCommandHandler.ExecuteAsync(new IncrementLinkRedirectsCountCommand
            {
                LinkId = result.LinkId
            });

            return Redirect(result.FullUrl);
        }

        private class Constants
        {
            public const string NoShortUrlError = "Не передан сокращенный URL";
        }
    }
}
