using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.src.Entity;

namespace user.src.DTO
{
    public class CategoryDTO
    {
        // when create product, we only need to provide name
        // id is provide by database
        public class CategoryCreateDto
        {
            public string Name { get; set; }
        }
        public class CategoryReadDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class CategoryReadDtoPro
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public List<Product> Products { get; set; }

        }
        public class CategoryUpdateDto
        {
            public string Name { get; set; }
        }


    }
}