using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static user.src.DTO.OrderDTO;

namespace user.src.Services.order
{
    public interface IOrderService
    {
        Task<OrderReadDto> CreateOneAsync(Guid UserId, OrderCreateDto createDto);
        Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(Guid userId);

    }
}