using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static user.src.DTO.ProductDTO;

namespace user.src.DTO
{
    public class OrderDetailDTO

    {
        public class OrderDetailCreateDto
        {
            public int Quantity { get; set; }
            public Guid ProductId { get; set; }
            // public ProductReadDto Product { get; set; }


        }
        public class OrderDetailReadDto
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
            public ProductReadDto Product { get; set; }
        }
    }
}