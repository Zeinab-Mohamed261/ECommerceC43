﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstrations
{
    public interface ICacheService
    {
        //Get 
        Task<string> GetAsync(string cacheKey);

        //Set
        Task SetAsync(string cacheKey, object value, TimeSpan TimeToLive);
    }
}
