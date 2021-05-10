using Basket.API.Models;
using System;

namespace Basket.API.Services
{
    public interface IBasketService
    {
        ShoppingCart UpdateBasket(ShoppingCart shoppingCart);
        ShoppingCart GetBasket(Guid userId);
        void DeleteBasket(Guid userId);
    }
}
