using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.src.Entity;
using static user.src.DTO.CategoryDTO;

namespace user.src.DTO
{
    public class ProductDTO
    {

        public class ProductListDto
        {
            public List<ProductReadDto> Products { get; set; }
            public int TotalCount { get; set; }
        }
        public class ProductReadDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public required decimal Price { get; set; }
            public required string ImageUrl { get; set; }
            public required string Description { get; set; }
            public CategoryReadDto Category { get; set; }
        }

        public class ProductCreateDto
        {
            public string Name { get; set; }
            public required decimal Price { get; set; }
            public required string ImageUrl { get; set; }
            public required string Description { get; set; }
            public Guid CategoryId { get; set; }

            // public required decimal StockQuantity { get; set; }

        }
        public class ProductUpdateDto
        {
            public string? Name { get; set; }
        }
    }
}