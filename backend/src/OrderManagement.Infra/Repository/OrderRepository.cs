using OrderManagement.Infra.DBDataContext;
using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Infra.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<Order> Insert(Order order)
        {
            await _appDbContext.Order.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _appDbContext.Order.ToListAsync();
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _appDbContext.Order.FindAsync(id);
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var orderDelete = await _appDbContext.Order.FindAsync(id);

            if (orderDelete is null)
            {
                return false;
            }

            _appDbContext.Order.Remove(orderDelete);

            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Order updateOrder)
        {
            var order = await _appDbContext.Order.FindAsync(updateOrder.Id);

            if (order is null)
            {
                return false;
            }

            order.Cliente = updateOrder.Cliente;
            order.Produto = updateOrder.Produto;
            order.Valor = updateOrder.Valor;
            order.Status = updateOrder.Status;

            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
