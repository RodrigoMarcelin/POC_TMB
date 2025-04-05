using Azure.Messaging.ServiceBus;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using OrderManagement.Domain.Entities;
using OrderManagement.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infra.ServiceBus
{
    public class BusUpdateQueue : IBusUpdateQueue
    {
        private readonly ServiceBusSender _serviceBusSender;

        public BusUpdateQueue(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }
        public async Task SendMessage(Order order)
        {
            await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(order.Id.ToString()));
        }
    }
}
