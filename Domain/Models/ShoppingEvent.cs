using System;
using CSharp.Choices;

namespace Domain.Models
{
    [AsChoice]
    public static partial class ShoppingEvent
    {
        public interface IShoppingEvent { }

        public record ShoppingSucceedEvent : IShoppingEvent
        {
            public DateTime date;
            public double totalPrice;

            internal ShoppingSucceedEvent(DateTime date, double totalPrice)
            {
                this.date = date;
                this.totalPrice = totalPrice;
            }
        }

        public record ShoppingFailedEvent : IShoppingEvent
        {
            public string error;

            internal ShoppingFailedEvent(string error)
            {
                this.error = error;
            }
        }
    }
}

