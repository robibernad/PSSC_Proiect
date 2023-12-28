using System;
using Data;
using Data.Models;

namespace Data.Repositories
{
    public class OrderHeadersRepository
    {
        private readonly ProiectPsscDb dbContext;

        public OrderHeadersRepository(ProiectPsscDb dbContext)
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
                var found = await dbContext.OrderHeaders.FindAsync(stringID);
                if (found == null)
                {
                    exist = false;
                }
            }

            return stringID;
        }

        public async Task<string> createNewOrderHeader(string clientId, string firstName, string lastName, long phoneNumber, string address, double total, string date)
        {
            OrderHeaderDTO orderHeader = new OrderHeaderDTO();
            orderHeader.OrderHeaderId = await GenerateRandomID();
            orderHeader.ClientId = clientId;
            orderHeader.FirstName = firstName;
            orderHeader.LastName = lastName;
            orderHeader.PhoneNumber = phoneNumber;
            orderHeader.Address = address;
            orderHeader.Date = date;
            orderHeader.Total = total;
            await dbContext.OrderHeaders.AddAsync(orderHeader);
            dbContext.SaveChanges();
            return orderHeader.OrderHeaderId;
        }
    }
}

