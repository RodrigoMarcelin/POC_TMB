using OrderManagement.Application.Mappers;
using OrderManagement.Domain.Repositories;
using OrderManagement.Shared.Commands;
using OrderManagement.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.UseCase
{
    public class GetOrderByIdUseCase
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> GetOrderById(Guid orderId)
        {
            try
            {
                var orderSelected = await _repository.GetById(orderId);
                if (orderSelected is not null)
                {
                    var orderSelectedDTO = OrderMapper.ToDto(orderSelected);

                    return new CommandResult(true, "", orderSelectedDTO);
                }

                return new CommandResult(true, "Não há nenhum pedido com essa descrição");

            } catch (Exception ex)
            {
                return new CommandResult(false, $"Erro ao selecionar o pedido pelo id: {ex.Message}");
            }
        }
    }
}
