using System.Collections.Generic;
using System.Threading.Tasks;
using MagicShortener.DataAccess.Mongo.Entities;

namespace MagicShortener.DataAccess
{
    /// <summary>
    /// Репозиторий для работы со ссылками
    /// </summary>
    public interface ILinksRepository
    {
        Task Create(Link link);
        Task<bool> Delete(string id);
        Task<Link> Get(string id);
        Task<IEnumerable<Link>> GetAll();
        Task<bool> Update(Link link);
    }
}