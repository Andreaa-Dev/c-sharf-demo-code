using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user.src.Entity
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        // test
        public List<Product> Products { get; set; }
    }
}