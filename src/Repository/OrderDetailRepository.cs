using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using user.src.Database;
using user.src.DTO;
using user.src.Entity;

namespace user.src.Repository
{
    public class OrderDetailRepository
    {
        protected readonly DbSet<OrderDetail> _orderDetail;
        protected readonly DatabaseContext _databaseContext;

        public OrderDetailRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _orderDetail = _databaseContext.Set<OrderDetail>();
        }
        public async Task<OrderDetail> CreateOneAsync(OrderDetail createObject)
        {
            await _orderDetail.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();
            return createObject;
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _orderDetail.AsNoTracking()
            //.Include(o => o.Product).ThenInclude(p => p.Category)
            .ToListAsync();
        }
    }
}