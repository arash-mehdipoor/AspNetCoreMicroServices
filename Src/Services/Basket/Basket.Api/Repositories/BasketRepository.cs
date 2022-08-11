using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }


        public async Task<ShoppingCart> Update(ShoppingCart basket)
        {
            var basketSerilizer = JsonConvert.SerializeObject(basket);
            await _redisCache.SetStringAsync(basket.UserName, basketSerilizer);
            return await GetBasket(basket.UserName);
        }


        public async Task Delete(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
