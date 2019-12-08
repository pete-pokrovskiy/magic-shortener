using MagicShortener.DataAccess.Mongo;
using MagicShortener.DataAccess.Mongo.Entitites;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace MagicShortener.DataAccess.Repositories
{

    public class CountersRepository : ICountersRepository
    {
        private readonly IMagicShortenerContext _context;

        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);

        public CountersRepository(IMagicShortenerContext context)
        {
            _context = context;
        }

        public async Task<long> GetNextLinkIdCounterValue()
        {
            return await GetNextCounterValue(Constants.LinkIdCounterName);
        }

        public async Task<long> GetNextCounterValue(string name)
        {
            // синхронизируем доступ к формированию значения счетчика,  чтобы гарантировать исключение ситуации получение одного и того же значения
            // при одновременном выполнении нескольких потоков
            await _mutex.WaitAsync();
            try
            {
                var counter = await _context.Counters.FindOneAndUpdateAsync(
                                        Builders<Counter>.Filter.Eq(a => a.Name, name),
                                        Builders<Counter>.Update.Inc(a => a.Value, 1));
                return counter.Value;
            }
            finally
            {
                _mutex.Release();
            }
        }
    }
}
