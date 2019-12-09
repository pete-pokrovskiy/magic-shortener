using MagicShortener.DataAccess;
using System;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Commands.Links.IncrementLinkRedirectsCount
{
    public class IncrementLinkRedirectsCountCommandHandler : ICommandHandler<IncrementLinkRedirectsCountCommand>
    {
        private readonly ILinksRepository _linksRepository;

        public IncrementLinkRedirectsCountCommandHandler(ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }

        public async Task ExecuteAsync(IncrementLinkRedirectsCountCommand command)
        {
            // TODO: для корректного ведения количества переходов здесь нужна синхронизация, аналогиная использованной в CountersRepository
            var link = await _linksRepository.Get(command.LinkId);

            // TODO: вынести проверку в валидационый декоратор
            if (link == null)
                throw new Exception($"Не найдена ссылка с идентификатором {command.LinkId}");

            link.LastTimeRedirected = DateTime.Now;
            link.RedirectsCount += 1;

            await _linksRepository.Update(link);
        }
    }
}
