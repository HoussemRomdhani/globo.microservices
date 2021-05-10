using Basket.API.Models;
using Basket.API.Services;
using MassTransit;
using Messaging.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Controllers
{
    [ApiController, Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketService basketService, IPublishEndpoint publishEndpoint)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }
       
        [HttpGet]
        public ActionResult<ShoppingCart> GetBasket(Guid userId)
        {
            var cart = _basketService.GetBasket(userId);
            return Ok(cart);
        }

        //[HttpGet, Route("count")]
        //public ActionResult<ShoppingCart> GetBasketCount(Guid userId)
        //{
        //    var cart = _basketService.GetBasket(userId);
        //    if (cart == null || cart.Items == null)
        //        return Ok(0);
        //    return Ok(cart.Items.Count);
        //}

        [HttpPost]
        public ActionResult UpdateBasket(ShoppingCart shoppingCart)
        {
            var cart = _basketService.UpdateBasket(shoppingCart);
            return Ok(cart);
        }

        [HttpPut]
        public ActionResult UpdateShoppingCartItem(Guid userId, [FromBody] ShoppingCartItem shoppingItem)
        {
            var basket = _basketService.GetBasket(userId);

            if (basket == null || basket.Items == null || basket.Items.Count == 0)
            {
                basket = new ShoppingCart
                {
                    UserId = userId,
                    Items = new List<ShoppingCartItem>
                    {
                        shoppingItem
                    }
                };
            }
            else
            {
                if (!basket.Items.Any(x => x.ProductId == shoppingItem.ProductId))
                    basket.Items.Add(shoppingItem);
                else
                {
                    foreach (var item in basket.Items)
                    {
                        if (item.ProductId == shoppingItem.ProductId)
                            item.Quantity = shoppingItem.Quantity;
                    }
                }
            }

            basket = _basketService.UpdateBasket(basket);

            return Ok(basket);
        }

        [HttpDelete]
        public ActionResult DeleteShoppingCartItem(Guid userId, Guid itemId)
        {
            var basket = _basketService.GetBasket(userId);

            if (basket == null || basket.Items == null || basket.Items.Count == 0)
                return Ok(basket);
            else
            {
                var itemToRemove = basket.Items.Single(item => item.ProductId == itemId);
                basket.Items.Remove(itemToRemove);
            }

            basket = _basketService.UpdateBasket(basket);

            return Ok(basket);
        }

        [HttpPatch]
        public ActionResult PatchBasket(Guid userId, [FromBody] JsonPatchDocument<ShoppingCart> shoppingCartPatch)
        {
            var basket = _basketService.GetBasket(userId);
            if (basket == null)
                basket = new ShoppingCart
                {
                    UserId = userId,
                   Items = new List<ShoppingCartItem>()
                };

            shoppingCartPatch.ApplyTo(basket);

            basket = _basketService.UpdateBasket(basket);

            return Ok(basket);

        }

        [HttpPost, Route("checkout")]
        public IActionResult Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get existing basket with total price            
            // Set TotalPrice on basketCheckout eventMessage
            // send checkout event to rabbitmq
            // remove the basket

            // get existing basket with total price
            var basket =  _basketService.GetBasket(basketCheckout.UserId);
            if (basket == null)
                return BadRequest();

            // send checkout event to rabbitmq
              basketCheckout.TotalPrice = basket.TotalPrice;

            var basketToSend = new BasketCheckoutEvent
            {
                UserId = basketCheckout.UserId,
                TotalPrice = basketCheckout.TotalPrice,
                FirstName = basketCheckout.FirstName,
                LastName = basketCheckout.LastName,
                EmailAddress = basketCheckout.EmailAddress,
                AddressLine = basketCheckout.AddressLine,
                Country = basketCheckout.Country,
                State = basketCheckout.State,
                ZipCode = basketCheckout.ZipCode,
                CardName = basketCheckout.CardName,
                CardNumber = basketCheckout.CardNumber,
                Expiration = basketCheckout.Expiration,
                CVV = basketCheckout.CVV,
                PaymentMethod = basketCheckout.PaymentMethod,

                Products = basket.Items.Select(x => new OrderProductEvent
                {
                    ProductId = x.ProductId,
                    Price = x.Price,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity
                }).ToList()
            };

             _publishEndpoint.Publish<BasketCheckoutEvent>(basketToSend);

            // remove the basket
             _basketService.DeleteBasket(basket.UserId);

            return Accepted();
        }
    }
}
