using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.Choices;
using static Domain.Models.Cart;

namespace Domain.Models
{
    [AsChoice]
    public static partial class Cart
    {
        public interface ICart { Client client { get; } };
        public record EmptyCart(Client client) : ICart;
        public record UnvalidatedCart(Client client, IReadOnlyCollection<UnvalidatedProduct> products) : ICart;
        public record ValidatedCart(Client client, IReadOnlyCollection<Product> products) : ICart;
        public record CalculatedCart(Client client, IReadOnlyCollection<Product> products, double price) : ICart;
        public record PaidCart(Client client, IReadOnlyCollection<Product> products, double finalPrice, DateTime data) : ICart;
    }
}
