using MagicShortener.DataAccess;
using MagicShortener.DataAccess.Mongo.Entities;
using MagicShortener.DataAccess.Repositories;
using MagicShortener.Logic.Services;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Commands.Links.CreateLink
{
    /// <summary>
    /// Обработчик команды создания ссылки
    /// </summary>
    public class CreateLinkCommandHandler : ICommandHandler<CreateLinkCommand>
    {
        private readonly ILinksRepository _linksRepository;
        private readonly ICountersRepository _countersRepository;

        private readonly IUrlShorteningService _urlShorteningService;

        public CreateLinkCommandHandler(ILinksRepository linksRepository,
            ICountersRepository countersRepository, IUrlShorteningService urlShorteningService)
        {
            _linksRepository = linksRepository;
            _countersRepository = countersRepository;
            _urlShorteningService = urlShorteningService;
        }

        public async Task ExecuteAsync(CreateLinkCommand command)
        {

            //var shortUrl = _urlShorteningService.Shorten(5);
            //var unshoretenedRecordId = _urlShorteningService.UnShorten(shortUrl);

            //if(id == unshoretenedRecordId)
            //{
            //    Debug.WriteLine("Success!");
            //}

            string nextLinkSequentialId = (await _countersRepository.GetNextLinkIdCounterValue()).ToString();

            command.Id = nextLinkSequentialId;

            await _linksRepository.Create(new Link
            {
                Id = command.Id,
                FullLink = command.FullLink,
            });
        }
    }
}
