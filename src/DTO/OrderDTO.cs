using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static user.src.DTO.OrderDetailDTO;

namespace user.src.DTO
{
    public class OrderDTO
    {
        public class OrderCreateDto
        {
            public IEnumerable<OrderDetailCreateDto> OrderDetails { get; set; }
        }
        public class OrderReadDto
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            // public User UserDetail { get; set; }
            public IEnumerable<OrderDetailReadDto> OrderDetails { get; set; }
        }
    }
}