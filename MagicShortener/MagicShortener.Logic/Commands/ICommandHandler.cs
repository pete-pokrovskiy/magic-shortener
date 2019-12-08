using System.Threading.Tasks;

namespace MagicShortener.Logic.Commands
{
    /// <summary>
    /// Базовый интерфейс для обработчиков команд
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
