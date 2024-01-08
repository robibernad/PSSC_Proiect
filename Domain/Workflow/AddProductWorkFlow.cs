using Data;
using Domain.Models;
using Domain.Operations;
using Domain.Workflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static Domain.Models.ShoppingEvent;
using static Domain.Models.Cart;
using Data.Models;


namespace Domain.Workflow
{
    public class AddProductWorkflow
    {
        private readonly ProiectPsscDb dbContext;

        public AddProductWorkflow(ProiectPsscDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IShoppingEvent> Execute(ProductDTO newProduct)
        {
            IShoppingEvent shoppingEvent = null;

            dbContext.Products.Add(newProduct);

            await dbContext.SaveChangesAsync();

            shoppingEvent = new ShoppingSucceedEvent();

            return shoppingEvent;


        }
    }
}


