using System.Threading.Tasks;
using AutoMapper;
using MagicShortener.API.Inputs;
using MagicShortener.Common.Helpers;
using MagicShortener.Logic.Commands;
using MagicShortener.Logic.Commands.Links.CreateLink;
using Microsoft.AspNetCore.Mvc;

namespace MagicShortener.API.Controllers
{
    [Route("api/links")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandHandler<CreateLinkCommand> _createLinkCommandHandler;

        public LinksController(IMapper mapper, 
            ICommandHandler<CreateLinkCommand> createLinkCommandHandler)
        {
            _mapper = mapper;
            _createLinkCommandHandler = createLinkCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateLinkRequest request)
        {
            //валидация тела запроса
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!(await UrlHelper.IsUrlReachable(request.Url)))
            {
                // TODO: в случае требования иной формы отображения результатов валидации имеет смысл добавить новый слой middleware 
                // - action-фильтр, в котором ModelState можно было бы обрабатывать определенным образом до старта выполенения метода API
                ModelState.AddModelError(nameof(CreateLinkRequest.Url), Constants.UnreachableUrlError);
                return BadRequest(ModelState);
            }

            //маппинг в команду
            var createCommand = _mapper.Map<CreateLinkCommand>(request);

            await _createLinkCommandHandler.ExecuteAsync(_mapper.Map<CreateLinkCommand>(request));

            return Created(GetEntityUri(createCommand.Id), createCommand.Id);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok();
        }

        // TODO: DELETE: /api/links/{linkId}
        // TODO: PUT: /api/links/{linkId}, Body = UpdateLinkRequest
        // TODOL PATCH: /api/links/{linkId} через JSON Patch

        private string GetEntityUri(string id)
        {
            return $"/api/links/{id}";

        }

        private class Constants
        {
            public const string UnreachableUrlError = "Url не существует или недоступен!";
        }


    }
}
