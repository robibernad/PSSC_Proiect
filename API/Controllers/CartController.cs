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

        [HttpPost("Add Order")]
    
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

        [HttpDelete("Delete Order")]
        public async Task<IActionResult> RemoveOrder(string orderHeaderId)
        {
            RemoveWorkflow removeWorkflow = new RemoveWorkflow(_dbContext);
            var result = await removeWorkflow.Execute(orderHeaderId);

            bool success = false;

            result.Match(
                whenShoppingFailedEvent: @event =>
                {
                    success = false;
                    return @event;
                },
                whenShoppingSucceedEvent: @event =>
                {
                    success = true;
                    return @event;
                }
            );

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("Modify Order")]
        public async Task<IActionResult> ModifyOrder([FromBody] ModifyOrderRequest request)
        {
            ModifyWorkFlow modifyWorkflow = new ModifyWorkFlow(_dbContext);

            var result = await modifyWorkflow.Execute(request.OrderHeaderId, request.UpdatedProducts, request.NewAddress);

            bool success = false;

            result.Match(
                whenShoppingFailedEvent: @event =>
                {
                    success = false;
                    return @event;
                },
                whenShoppingSucceedEvent: @event =>
                {
                    success = true;
                    return @event;
                }
            );

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        public class ModifyOrderRequest
        {
            public string OrderHeaderId { get; set; }
            public List<UnvalidatedProduct> UpdatedProducts { get; set; }
            public string NewAddress { get; set; }
        }
    }
}
