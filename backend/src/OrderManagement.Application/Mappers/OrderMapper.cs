using OrderManagement.Application.DTO;
using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Mappers
{
    public class OrderMapper
    {
        public static OrderDTO ToDto(Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                Cliente = order.Cliente,
                Produto = order.Produto,
                Valor = order.Valor,
                Status = order.Status,
                DataCriacao = order.DataCriacao
            };
        }

        public static IEnumerable<OrderDTO> ToDtoList(IEnumerable<Order> orders)
        {
            return orders.Select(order => ToDto(order));
        }
    }
}
