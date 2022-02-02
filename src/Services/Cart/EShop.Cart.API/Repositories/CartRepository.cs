using Eshop.Libraries.Serializer;
using EShop.Cart.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace EShop.Cart.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;
        private readonly ISerializer _serializer;

        private string _cache_key_prefix = "user_cart_";

        public CartRepository(IDistributedCache redisCache, ISerializer serializer)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task DeleteCart(int userId)
        {
            await _redisCache.RemoveAsync(_cache_key_prefix + userId);
        }

        public async Task<ShoppingCart> GetCart(int userId)
        {
            var cart = await _redisCache.GetStringAsync(_cache_key_prefix + userId);
            if(string.IsNullOrEmpty(cart)) return null;

            var deserializedCart = _serializer.DeserializeObject<ShoppingCart>(cart);
            return deserializedCart;
        }

        public async Task<ShoppingCart> UpdateCart(ShoppingCart shoppingCart)
        {
            var serializedCart = _serializer.SerializeObject(shoppingCart);
            var userId = shoppingCart.UserId;

            await _redisCache.SetStringAsync(_cache_key_prefix + userId, serializedCart);

            return await GetCart(userId);
        }
    }
}
