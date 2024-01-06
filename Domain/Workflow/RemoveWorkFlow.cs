using Data;
using Data.Models;
using Domain.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.ShoppingEvent;

namespace Domain.Workflow
{
public class RemoveWorkflow
    {
        private readonly ProiectPsscDb dbContext;

        public RemoveWorkflow(ProiectPsscDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IShoppingEvent> Execute(string orderHeaderId)
        {
            IShoppingEvent shoppingEvent = null;

            OrderHeaderDTO orderHeaderToDelete = await dbContext.OrderHeaders
                .FirstOrDefaultAsync(o => o.OrderHeaderId == orderHeaderId);

            if (orderHeaderToDelete == null)
            {
                shoppingEvent = new ShoppingFailedEvent("Order not found");
                return shoppingEvent;
            }

            var productQuantities = dbContext.OrderLines
            .Where(ol => ol.OrderHeaderId == orderHeaderToDelete.OrderHeaderId)
            .ToDictionary(ol => ol.ProductId, ol => ol.Quantity);

            dbContext.OrderHeaders.Remove(orderHeaderToDelete);

            var orderLinesToDelete = dbContext.OrderLines
                .Where(ol => ol.OrderHeaderId == orderHeaderToDelete.OrderHeaderId);

            dbContext.OrderLines.RemoveRange(orderLinesToDelete);

            await dbContext.SaveChangesAsync();

            foreach (var productId in productQuantities.Keys)
            {
                var quantityToAdd = productQuantities[productId];
                await dbContext.UpdateStockAsync(productId, quantityToAdd);
            }

            shoppingEvent = new ShoppingSucceedEvent();

            return shoppingEvent;
        }
    }

}