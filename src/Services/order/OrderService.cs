using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using user.src.Entity;
using user.src.Repository;
using static user.src.DTO.OrderDTO;

namespace user.src.Services.order
{
    public class OrderService : IOrderService
    {
        protected readonly OrderRepository _orderRepo;
        protected readonly IMapper _mapper;
        public OrderService(OrderRepository repo, IMapper mapper)
        {
            _orderRepo = repo;
            _mapper = mapper;
        }
        public async Task<OrderReadDto> CreateOneAsync(Guid UserId, OrderCreateDto createDto)
        {
            var order = _mapper.Map<OrderCreateDto, Order>(createDto);
            order.UserId = UserId;
            await _orderRepo.CreateOneAsync(order);
            return _mapper.Map<Order, OrderReadDto>(order);
        }

        // get order by user Id
        public async Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            // Fetch orders from the repository by userId
            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId);

            // Map the collection of Order to a collection of OrderReadDto
            var orderList = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderReadDto>>(orders);

            return orderList;

        }
    }
}