using Microsoft.AspNetCore.Mvc;
using Data;
using Domain.Models;
using Domain.Workflow;
using static Domain.Models.ShoppingEvent;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {

        private readonly ProiectPsscDb _dbContext;
        public CartController(ProiectPsscDb context)
        {
            _dbContext = context;
        }

        public class OrderRequest
        {
            public Client Client { get; set; }
            public List<UnvalidatedProduct> Products { get; set; }
        }

        [HttpPost]
    
        public async Task<IActionResult> AddOrder([FromBody]OrderRequest request)
        {
            AddOrderWorkFlow workflow = new AddOrderWorkFlow(_dbContext);
            var res = await workflow.execute(request.Client, request.Products);
            bool succed=false;
            res.Match(
                whenShoppingFailedEvent: @event =>
                {
                    succed = false;
                    return @event;
                },
                whenShoppingSucceedEvent:@event =>
                {
                    succed = true;
                    return @event;
                }

                ) ;

            if (succed == false)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
