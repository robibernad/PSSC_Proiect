using Data;
using Domain.Models;
using Domain.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Cart;
using static Domain.Models.ShoppingEvent;

namespace Domain.Workflow
{
    public class AddOrderWorkFlow
    {
        public readonly ProiectPsscDb dbContext;
        CartOperation cartOperation;
        public AddOrderWorkFlow(ProiectPsscDb dbContext)
        {
            this.dbContext = dbContext;
            cartOperation = new CartOperation(dbContext);
        }

        public async Task<IShoppingEvent> execute(Client client, List<UnvalidatedProduct> unvalidatedProducts)
        {
            IShoppingEvent shoppingEvent = null;
            ICart cart = new EmptyCart(client);
            cart = cartOperation.addProductToCart((EmptyCart)cart, unvalidatedProducts);
            if (cart is not UnvalidatedCart)
            {
                shoppingEvent = new ShoppingFailedEvent("Cart is empty");
                return shoppingEvent;
            }
            cart = await cartOperation.validateCart((UnvalidatedCart)cart);
            if (cart is not ValidatedCart)
            {
                shoppingEvent = new ShoppingFailedEvent("Cart is unvalidate");
                return shoppingEvent;
            }
            cart = cartOperation.calculateCart((ValidatedCart)cart);
            if (cart is not CalculatedCart)
            {
                shoppingEvent = new ShoppingFailedEvent("Cart is validated but couldn't be calculated");
                return shoppingEvent;
            }
            cart = cartOperation.payCart((CalculatedCart)cart);
            if (cart is PaidCart)
            {
                await cartOperation.sendCart((PaidCart)cart);
            }

            cart.Match(
                    whenEmptyCart: @event =>
                    {
                        shoppingEvent = new ShoppingFailedEvent("Cart is empty");
                        return @event;
                    },
                    whenUnvalidatedCart: @event =>
                    {
                        shoppingEvent = new ShoppingFailedEvent("Cart is unvalidate");
                        return @event;
                    },
                    whenValidatedCart: @event =>
                    {
                        shoppingEvent = new ShoppingFailedEvent("Cart is validate but we have internal server problems");
                        return @event;
                    },
                    whenCalculatedCart: @event =>
                    {
                        shoppingEvent = new ShoppingFailedEvent("Cart is calculated but is not payed");
                        return @event;
                    },
                    whenPaidCart: @event =>
                    {
                        shoppingEvent = new ShoppingSucceedEvent(@event.data, @event.finalPrice);
                        return @event;
                    }
            );
            return shoppingEvent!;
        }    
    }
}
