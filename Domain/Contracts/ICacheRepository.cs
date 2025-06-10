﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        // Get
        Task<string?> GetAsync(string cacheKey);

        // Set
        Task SetAsync(string cacheKey, string value, TimeSpan timeSpan);
    }
}
