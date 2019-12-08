using MagicShortener.Logic.Commands;
using MagicShortener.Logic.Commands.Links.IncrementLinkRedirectsCount;
using MagicShortener.Logic.Queries;
using MagicShortener.Logic.Queries.Links.ConvertShortUrlToFull;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.API.Controllers
{
    [Route("r")]
    public class RedirectController : Controller
    {
        private readonly ICommandHandler<IncrementLinkRedirectsCountCommand> _incrementLinkRedirectsCountCommandHandler;
        private readonly IQueryHandler<ConvertShortUrlToFullQuery, ConvertShortUrlToFullQueryResult> _convertShortUrlToFullQueryHandler;

        public RedirectController(
            ICommandHandler<IncrementLinkRedirectsCountCommand> incrementLinkRedirectsCountCommandHandler,
            IQueryHandler<ConvertShortUrlToFullQuery, ConvertShortUrlToFullQueryResult> convertShortUrlToFullQueryHandler)
        {
            _incrementLinkRedirectsCountCommandHandler = incrementLinkRedirectsCountCommandHandler;
            _convertShortUrlToFullQueryHandler = convertShortUrlToFullQueryHandler;
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectToFullUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                return BadRequest(Constants.NoShortUrlError);

            
            var result = await _convertShortUrlToFullQueryHandler.ExecuteAsync(new ConvertShortUrlToFullQuery
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
