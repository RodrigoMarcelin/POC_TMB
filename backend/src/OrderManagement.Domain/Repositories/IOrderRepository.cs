using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Insert(Order order);

        Task<List<Order>> GetAll();

        Task<Order> GetById(Guid id);

        Task<bool> DeleteById(Guid id);

        Task<bool> UpdateAsync(Order updateOrder);

    }
}
