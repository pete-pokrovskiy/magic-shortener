using MagicShortener.DataAccess.Mongo;
using MagicShortener.DataAccess.Mongo.Entities;
using MagicShortener.DataAccess.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.DataAccess
{
    public class LinksRepository : ILinksRepository
    {
        private readonly IMagicShortenerContext _context;

        public LinksRepository(IMagicShortenerContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Link>> GetAll()
        {
            return await _context
                            .Links
                            .Find(_ => true)
                            .ToListAsync();
        }
        public async Task<Link> Get(string id)
        {
            FilterDefinition<Link> filter = Builders<Link>.Filter.Eq(m => m.Id, id);
            return await _context
                    .Links
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(Link link)
        {
            await _context.Links.InsertOneAsync(link);
        }
        public async Task<bool> Update(Link link)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Links
                        .ReplaceOneAsync(
                            filter: g => g.Id == link.Id,
                            replacement: link);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Link> filter = Builders<Link>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Links
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }

}
