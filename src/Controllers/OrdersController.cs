using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user.src.Services.order;
using static user.src.DTO.OrderDTO;

namespace user.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        protected readonly IOrderService _orderService;
        protected readonly IAuthorizationService _authorization;
        public OrdersController(IOrderService orderService, IAuthorizationService authorization)
        {
            _orderService = orderService;
            _authorization = authorization;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderReadDto>> CreateOneAsync([FromBody] OrderCreateDto orderCreateDto)
        {
            var authenticatedClaims = HttpContext.User;
            var userId = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userGuid = new Guid(userId);
            return await _orderService.CreateOneAsync(userGuid, orderCreateDto);
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);

            return Ok(orders);
        }
    }
}