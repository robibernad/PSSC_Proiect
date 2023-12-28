using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using static Domain.Models.Cart;
using Domain.Models;
using Domain.Mappers;


namespace Domain.Operations
{
    public class CartOperation
    {
        private readonly ProiectPsscDb dbContext;
        ProductRepository productsRepository;
        OrderHeadersRepository orderHeadersRepository;
        OrderLinesRepository orderLinesRepository;

        public CartOperation(ProiectPsscDb dbContext)
        {
            this.dbContext = dbContext;
            productsRepository = new ProductRepository(dbContext);
            orderHeadersRepository = new OrderHeadersRepository(dbContext);
            orderLinesRepository = new OrderLinesRepository(dbContext);
        }
        public static ICart addProductToCart(EmptyCart emptyCart, IReadOnlyCollection<UnvalidatedProduct> unvalidatedProducts)
        {
            if (unvalidatedProducts.Count > 0)
            {
                return new UnvalidatedCart(emptyCart.client, unvalidatedProducts);
            }
            else
            {
                return emptyCart;
            }
        }

        public async Task<ICart> validateCart(UnvalidatedCart unvalidatedCart)
        {
            bool isValid = true;
            List<Product> cartProducts = new List<Product>();
            foreach (var product in unvalidatedCart.products)
            {
                Product validProduct = ProductMapper.MapToProduct(await productsRepository.TryGetProduct(product.Id, product.Quantity));
                if (validProduct != null)
                {
                    cartProducts.Add(validProduct);
                }
                else
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                return new ValidatedCart(unvalidatedCart.client, cartProducts);
            }
            else
            {
                return unvalidatedCart;

            }
        }

        public static CalculatedCart calculateCart(ValidatedCart validatedCart)
        {
            double total = validatedCart.products.Sum(product => product.Price);
            return new CalculatedCart(validatedCart.client, validatedCart.products, total);
        }

        public static ICart payCart(CalculatedCart calculatedCart)
        {
            bool payed = true;
            if (payed)
            {
                return new PaidCart(calculatedCart.client, calculatedCart.products, calculatedCart.price, DateTime.Now);
            }
            else
            {
                return calculatedCart;
            }
        }

        public async Task sendCart(PaidCart cart)
        {
            string orderId = await orderHeadersRepository.createNewOrderHeader(cart.client.clientId.ToString(), cart.client.firstName, cart.client.lastName, cart.client.phoneNumber, cart.client.address, cart.finalPrice, cart.data.ToString());
            foreach (var product in cart.products)
            {
                await productsRepository.TryRemoveStoc(product.productId, product.Quantity);
                await orderLinesRepository.AddProductLine(product.productId, product.Quantity, product.Price, orderId);
            }
        }
    }
}
