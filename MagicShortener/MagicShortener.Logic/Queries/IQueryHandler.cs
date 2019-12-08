using System.Threading.Tasks;

namespace MagicShortener.Logic.Queries
{
    /// <summary>
    /// Базовый интерфейс для обработчиков запросов
    /// </summary>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
        where TResult : IQueryResult
    {
        Task<TResult> ExecuteAsync(TQuery query);
    }
}