﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.Caching.Base
{
    public interface ICacheService
    {
        T Get<T>(string key);

        string Get(string key);
        List<T> GetByPattern<T>(string pattern);
        IEnumerable<RedisKey> GetKeysByPattern(string pattern = "*");
        void DeleteKeysByPattern(string pattern = "*");
        bool Any(string key);
        void Set<T>(string cacheKey, T value);
        void Remove(string cacheKey);
        List<T> DeleteExpiredCaches<T>(IEnumerable<RedisKey> keyList);
        void Clear();
    }
}
