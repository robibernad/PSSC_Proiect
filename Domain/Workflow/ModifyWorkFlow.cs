using Data.Models;
using Data;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Domain.Models.ShoppingEvent;
using Microsoft.EntityFrameworkCore;

namespace Domain.Workflow
{
    public class ModifyWorkFlow
    {
        private readonly ProiectPsscDb _dbContext;

        public ModifyWorkFlow(ProiectPsscDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IShoppingEvent> Execute(string orderHeaderId, List<UnvalidatedProduct> updatedProducts, string newAddress)
        {
            IShoppingEvent shoppingEvent = null;

            OrderHeaderDTO orderHeaderToUpdate = await _dbContext.OrderHeaders
                .FirstOrDefaultAsync(o => o.OrderHeaderId == orderHeaderId);

            if (orderHeaderToUpdate == null)
            {
                shoppingEvent = new ShoppingFailedEvent("Order not found");
                return shoppingEvent;
            }

            orderHeaderToUpdate.Address = newAddress;

            var orderLinesToUpdate = await _dbContext.OrderLines
                .Where(ol => ol.OrderHeaderId == orderHeaderId)
                .ToListAsync();

            foreach (var orderLine in orderLinesToUpdate)
            {
                var productToUpdate = _dbContext.Products.FirstOrDefault(pr => pr.ProductId == orderLine.ProductId);

                if (productToUpdate != null)
                {
                    if (orderLine.Quantity > updatedProducts.FirstOrDefault(p => p.Id == orderLine.ProductId)?.Quantity)
                    {
                        productToUpdate.Quantity += orderLine.Quantity - updatedProducts.First(p => p.Id == orderLine.ProductId).Quantity;
                    }
                    else if (orderLine.Quantity < updatedProducts.FirstOrDefault(p => p.Id == orderLine.ProductId)?.Quantity)
                    {
                        productToUpdate.Quantity -= updatedProducts.First(p => p.Id == orderLine.ProductId).Quantity - orderLine.Quantity;
                    }

                    orderLine.Quantity = updatedProducts.FirstOrDefault(p => p.Id == orderLine.ProductId)?.Quantity ?? 0;
                    orderLine.TotalPrice = orderLine.Quantity * productToUpdate.Price;
                }
            }

            orderHeaderToUpdate.Total = orderLinesToUpdate.Sum(ol => ol.TotalPrice);

            await _dbContext.SaveChangesAsync();

            shoppingEvent = new ShoppingSucceedEvent();

            return shoppingEvent;
        }
    }
}
