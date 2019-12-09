using MagicShortener.DataAccess.Mongo;
using MagicShortener.DataAccess.Mongo.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMagicShortenerContext _context;

        public UsersRepository(IMagicShortenerContext context)
        {
            _context = context;

        }

        public async Task<User> GetByLogin(string login)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(m => m.Login, login);
            return await _context
                    .Users
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
    }
}
