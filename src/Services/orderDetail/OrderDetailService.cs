using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using user.src.DTO;
using user.src.Entity;
using user.src.Repository;
using static user.src.DTO.OrderDetailDTO;

namespace user.src.Services.orderDetail
{
    public class OrderDetailService : IOrderDetailService
    {
        protected readonly OrderDetailRepository _orderDetailRepo;
        protected readonly IMapper _mapper;
        public OrderDetailService(OrderDetailRepository orderDetailRepo, IMapper mapper)
        {
            _orderDetailRepo = orderDetailRepo;
            _mapper = mapper;
        }


        // create order detail
        public async Task<OrderDetailReadDto> CreateOneAsync(OrderDetailCreateDto createDto)
        {
            var orderDetail = _mapper.Map<OrderDetailCreateDto, OrderDetail>(createDto);
            var orderDetailCreated = await _orderDetailRepo.CreateOneAsync(orderDetail);
            return _mapper.Map<OrderDetail, OrderDetailReadDto>(orderDetailCreated);
        }


    }
}