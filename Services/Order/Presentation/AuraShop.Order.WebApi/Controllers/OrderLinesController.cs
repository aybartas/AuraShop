using AuraShop.Order.Application.Features.CQRS.Commands.OrderLine;
using AuraShop.Order.Application.Features.CQRS.Handlers.OrderLine;
using AuraShop.Order.Application.Features.CQRS.Queries.OrderLine;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLinesController : ControllerBase
    {
        private readonly GetOrderLineQueryHandler _getOrderLineQueryHandler;
        private readonly GetOrderLineByIdQueryHandler _getOrderLineByIdQueryHandler;
        private readonly RemoveOrderLineCommandHandler _removeOrderLineCommandHandler;
        private readonly UpdateOrderLineCommandHandler _updateOrderLineCommandHandler;
        private readonly CreateOrderLineCommandHandler _createOrderLineCommandHandler;

        public OrderLinesController(GetOrderLineQueryHandler getOrderLineQueryHandler, GetOrderLineByIdQueryHandler getOrderLineByIdQueryHandler, RemoveOrderLineCommandHandler removeOrderLineCommandHandler, UpdateOrderLineCommandHandler updateOrderLineCommandHandler, CreateOrderLineCommandHandler createOrderLineCommandHandler)
        {
            _getOrderLineQueryHandler = getOrderLineQueryHandler;
            _getOrderLineByIdQueryHandler = getOrderLineByIdQueryHandler;
            _removeOrderLineCommandHandler = removeOrderLineCommandHandler;
            _updateOrderLineCommandHandler = updateOrderLineCommandHandler;
            _createOrderLineCommandHandler = createOrderLineCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderLinees()
        {
            var values = await _getOrderLineQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderLineById(int id)
        {
            var request = new GetOrderLineByIdQuery
            {
                Id = id
            };

            var value = await _getOrderLineByIdQueryHandler.Handle(request);
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrderLine(int id)
        {
            var command = new RemoveOrderLineCommand()
            {
                Id = id
            };

            await _removeOrderLineCommandHandler.Handle(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderLine(CreateOrderLineCommand command)
        {
            await _createOrderLineCommandHandler.Handle(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderLine(UpdateOrderLineCommand command)
        {
            await _updateOrderLineCommandHandler.Handle(command);
            return Ok();
        }
    }
}
