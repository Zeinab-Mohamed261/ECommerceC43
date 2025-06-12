﻿using Domain.Contracts;
using ServicesAbstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string> GetAsync(string cacheKey)
        {
           return await cacheRepository.GetAsync(cacheKey);
        }

        public async Task SetAsync(string cacheKey, object cachevalue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(cachevalue);
            await cacheRepository.SetAsync(cacheKey, value, TimeToLive);
        }
    }
}
