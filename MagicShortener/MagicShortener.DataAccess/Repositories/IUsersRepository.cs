using System.Threading.Tasks;
using MagicShortener.DataAccess.Mongo.Entities;

namespace MagicShortener.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetByLogin(string login);
    }
}