using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using backend.Interfaces;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace backend.Services
{
    public class RedisCacheService : IRedisCacheService
    {

        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheService> _logger;

        public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }


        public async Task<T?> GetDataAsync<T>(string key)
        {
            try
            {
                var data = await _cache.GetStringAsync(key);

                if (data == null)
                {
                    return default(T);
                }

                return JsonSerializer.Deserialize<T>(data, _seralizerOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting data from cache: {ex.Message}");
                return default(T);
            }
        }

        public async Task SetDataAsync<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            try
            {
                if (data == null)
                {
                    _logger.LogWarning($"Attempted to set null data in cache with key: {key}");
                    return;
                }

                await _cache.SetStringAsync(key, JsonSerializer.Serialize(data, _seralizerOptions), options);
                _logger.LogInformation($"Data set in cache with key: {key}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error setting data in cache: {ex.Message}");
                return;
            }
        }


        private static readonly JsonSerializerOptions _seralizerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}