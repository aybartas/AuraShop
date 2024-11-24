﻿namespace AuraShop.Basket.Settings;

public interface IRedisService
{
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task<T?> GetAsync<T>(string key);
    Task RemoveAsync(string key);
    Task<bool> KeyExistsAsync(string key);
}