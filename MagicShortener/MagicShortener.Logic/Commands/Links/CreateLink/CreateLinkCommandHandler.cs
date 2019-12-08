using MagicShortener.DataAccess;
using MagicShortener.DataAccess.Mongo.Entities;
using MagicShortener.DataAccess.Repositories;
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

        public CreateLinkCommandHandler(
            ILinksRepository linksRepository,
            ICountersRepository countersRepository)
        {
            _linksRepository = linksRepository;
            _countersRepository = countersRepository;
        }

        public async Task ExecuteAsync(CreateLinkCommand command)
        {
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
