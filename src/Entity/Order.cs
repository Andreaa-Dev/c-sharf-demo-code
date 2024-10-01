using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user.src.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }


    }
}