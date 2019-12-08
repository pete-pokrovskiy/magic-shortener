using System.Threading.Tasks;

namespace MagicShortener.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий для работы со счетчиками для автоинкремента
    /// </summary>
    public interface ICountersRepository
    {
        Task<long> GetNextCounterValue(string name);
        Task<long> GetNextLinkIdCounterValue();
    }
}