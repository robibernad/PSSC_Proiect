using System;
using Data;
using Data.Models;

namespace Data.Repositories
{
    public class OrderLinesRepository
    {
        private readonly ProiectPsscDb dbContext;

        public OrderLinesRepository(ProiectPsscDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GenerateRandomID()
        {
            Guid guid;
            string stringID = "";
            bool exist = true;
            while (exist)
            {
                guid = Guid.NewGuid();
                stringID = guid.ToString();
                var found = await dbContext.OrderLines.FindAsync(stringID);
                if (found == null)
                {
                    exist = false;
                }
            }

            return stringID;
        }

        public async Task AddProductLine(string productId, int quantity, double price, string orderID)
        {
            OrderLineDTO orderLine = new OrderLineDTO();
            orderLine.OrderLineId = orderID;
            orderLine.ProductId = productId;
            orderLine.Quantity = quantity;
            orderLine.TotalPrice = price;
            orderLine.OrderLineId = await GenerateRandomID();
            await dbContext.OrderLines.AddAsync(orderLine);
            dbContext.SaveChanges();
        }
    }
}

