using Common.Services.Interfaces;
using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllAsync(cancellationToken);

            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByIdAsync(id, cancellationToken);

            return Ok(order);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(OrderDto orderDto, CancellationToken cancellationToken)
        {
            await _orderService.CreateAsync(orderDto, cancellationToken);

            return Ok("Order has been created =)");
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(OrderDto orderDto, CancellationToken cancellationToken)
        {
            await _orderService.UpdateAsync(orderDto, cancellationToken);

            return Ok("Order has been updated =)");
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await _orderService.DeleteByIdAsync(id, cancellationToken);

            return Ok("Order has been deleted =(");
        }
    }
}
