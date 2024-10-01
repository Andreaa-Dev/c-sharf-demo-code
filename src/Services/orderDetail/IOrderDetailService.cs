using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static user.src.DTO.OrderDetailDTO;

namespace user.src.Services.orderDetail
{
    public interface IOrderDetailService
    {
        Task<OrderDetailReadDto> CreateOneAsync(OrderDetailCreateDto createDto);

    }
}