using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user.src.Entity
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        // to get product detail
        // if not then it will be null
        public Product Product { get; set; }

    }
}