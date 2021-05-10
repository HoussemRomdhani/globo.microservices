using Messaging.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; }

        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public string PaymentMethod { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<OrderProduct> Products { get; set; }

        public Order()
        {
            OrderId = Guid.NewGuid();
        }

        public Order(BasketCheckoutEvent message)
        {
            OrderId = Guid.NewGuid();

            UserId = message.UserId;

            CreatedAt = DateTime.Now;

            TotalPrice = message.TotalPrice;

            FirstName = message.FirstName;
            LastName = message.LastName;
            EmailAddress = message.EmailAddress;
            AddressLine = message.AddressLine;
            Country = message.Country;
            State = message.State;
            ZipCode = message.ZipCode;

            CardName = message.CardName;
            CardNumber = message.CardNumber;
            Expiration = message.Expiration;
            CVV = message.CVV;
            PaymentMethod = message.PaymentMethod;

        }
    }
}
