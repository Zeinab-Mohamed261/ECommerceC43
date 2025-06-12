using Domain.Contracts;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Reposotories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string cacheKey)
        {
            var cacheValue =await _database.StringGetAsync(cacheKey);
            return cacheValue.IsNullOrEmpty ? null : cacheKey.ToString();
        }

        public async Task SetAsync(string cacheKey, string value, TimeSpan timeSpan)
        {
           await _database.StringSetAsync(cacheKey, value, timeSpan);
        }
    }
}
