using System.Threading.Tasks;
using AutoMapper;
using MagicShortener.API.Inputs;
using MagicShortener.Common.Helpers;
using MagicShortener.Logic.Commands;
using MagicShortener.Logic.Commands.Links.CreateLink;
using MagicShortener.Logic.Queries;
using MagicShortener.Logic.Queries.Links.GetAllLinks;
using MagicShortener.Logic.Queries.Links.GetLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicShortener.API.Controllers
{
    // TODO:Добавляем авторизационный атрибут, чтобы включить проверку креденшиалов
    // [Authorize]
    // TODO: для того, чтобы анонимные пользователи могли без аккаунта создавать короткие ссылки, добавляем к соответствующим методам атрибут [AllowAnonymous]
    // далее, в завимиости от того, есть ли валидный токен в запросе пользователия, он либо аутентифицирован, тогда возвращаем ему список его ссылок,
    // либо нет, в таком случае на лету создаем короткуе ссылку и возвращаем ему в куках ответа
    // Для хранения сокращенных ссылок неавторизованного пользователя предпочтительным является как раз хранение состояи в куках
    // то есть во взаимодействии запрос-ответ в куках всегда лежит текущий перечень сокращенных ссылок - при формировании нового значение оно просто добавляется в конец
    // в случае необходимости явно хранить такое состояние на сервере, в куках или одном из заголовоков можно хранить присвоенный после инициализации временный токен
    // по этому временному токену хранить ссылки в хранилище - но у такого рода записей должно быть ограниченное время жизни + ограниченное max количество
    // по истечении срока, какое-то периодическое задание должно их подчищать
    [Route("api/links")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandHandler<CreateLinkCommand> _createLinkCommandHandler;
        private readonly IQueryHandler<GetLinkQuery, GetLinkQueryResult> _getLinkQueryHandler;
        private readonly IQueryHandler<GetAllLinksQuery, GetAllLinksQueryResult> _getAllLinksQueryHandler;

        public LinksController(IMapper mapper, 
            ICommandHandler<CreateLinkCommand> createLinkCommandHandler,

            IQueryHandler<GetLinkQuery, GetLinkQueryResult> getLinkQueryHandler,
            IQueryHandler<GetAllLinksQuery, GetAllLinksQueryResult> getAllLinksQueryHandler)
        {
            _mapper = mapper;
            _createLinkCommandHandler = createLinkCommandHandler;
            _getLinkQueryHandler = getLinkQueryHandler;
            _getAllLinksQueryHandler = getAllLinksQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllLinksQueryHandler.ExecuteAsync(new GetAllLinksQuery());
            return Ok(result.Links);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(Constants.NoIdError);

            var result = await _getLinkQueryHandler.ExecuteAsync(new GetLinkQuery { LinkId = id });

            if (result == null)
                return NotFound();

            return Ok(result.Link);
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

            await _createLinkCommandHandler.ExecuteAsync(createCommand);

            return Created(GetEntityUri(createCommand.Id), createCommand.Id);

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
            public const string NoIdError = "Не передан идентификатор ссылки!";
        }


    }
}
