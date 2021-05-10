using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDistributedCache _cache;

        public BasketService(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public ShoppingCart UpdateBasket(ShoppingCart shoppingCart)
        {
            var count = shoppingCart.Items != null ? shoppingCart.Items.Count : 0;
            var totalPrice = shoppingCart.Items != null ?
                              shoppingCart.Items.Select(item => item.Price * item.Quantity).Sum()
                               : 0;

            var shoppingCartToUpdate = new ShoppingCart
            {
                UserId = shoppingCart.UserId,
                Items = shoppingCart.Items,
                Count = count,
                TotalPrice = totalPrice
            };
            _cache.SetString(shoppingCartToUpdate.UserId.ToString(), JsonConvert.SerializeObject(shoppingCartToUpdate));
            return  GetBasket(shoppingCartToUpdate.UserId);
        }

        public ShoppingCart GetBasket(Guid userId)
        {
            var basket = _cache.GetString(userId.ToString());

            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public void DeleteBasket(Guid userId)
        {
             _cache.Remove(userId.ToString());
        }
    }
}
