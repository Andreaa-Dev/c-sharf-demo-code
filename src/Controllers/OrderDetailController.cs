using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using user.src.DTO;
using user.src.Services.orderDetail;
using static user.src.DTO.OrderDetailDTO;

namespace user.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderDetailController : ControllerBase
    {

        protected readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService service)
        {
            _orderDetailService = service;
        }
        [HttpPost]
        public async Task<ActionResult<OrderDetailReadDto>> CreateOneController([FromBody] OrderDetailCreateDto createDto)
        {
            var orderDetailCreated = await _orderDetailService.CreateOneAsync(createDto);
            return Ok(orderDetailCreated);
        }
    }
}