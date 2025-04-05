using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Shared.Interface
{
    public interface IBusUpdateQueue
    {
        Task SendMessage(Order order);
    }
}
