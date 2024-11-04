using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using user.src.Database;
using user.src.Entity;

namespace user.src.Repository
{
    public class OrderRepository
    {
        protected readonly DbSet<Order> _orders;
        protected readonly DbSet<Product> _products;
        protected readonly DbSet<OrderDetail> _orderDetails;
        protected readonly DatabaseContext _databaseContext;
        public OrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _orders = _databaseContext.Set<Order>();
            _orderDetails = _databaseContext.Set<OrderDetail>();
            _products = _databaseContext.Set<Product>();
        }

        public async Task<Order?> CreateOneAsync(Order createObject)
        {
            await _orders.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();

            // await _orders.Entry(createObject).Collection(o => o.OrderDetails).LoadAsync();
            // foreach (var detail in createObject.OrderDetails)
            // {
            //     await _databaseContext.Entry(detail).Reference(od => od.Product).LoadAsync();
            // }

            // return createObject;

            // way2
            var orderWithDetails = await _orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .FirstOrDefaultAsync(o => o.Id == createObject.Id);

            return orderWithDetails;


        }


        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _databaseContext.Order
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Where(o => o.UserId == userId)
            .ToListAsync();
        }
    }
}