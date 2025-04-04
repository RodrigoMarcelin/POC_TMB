using OrderManagement.Application.DTO;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using OrderManagement.Shared.Commands;
using OrderManagement.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.UseCase
{
    public class UpdateOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public UpdateOrderUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> UpdateOrder(Guid id, OrderDTO command)
        {
            try
            {
                var updateEntity = new Order(command.Id, command.Cliente, command.Produto, command.Valor, command.Status, command.DataCriacao);
                var resultUpdateOrder = await _repository.UpdateAsync(updateEntity);

                if (resultUpdateOrder)
                {
                    return new CommandResult(true, "Pedido atualizado com sucesso.");
                }
                else
                {
                    return new CommandResult(false, "Falha ao atualizar o pedido.");
                }
            } catch (Exception ex)
            {
                return new CommandResult(false, $"Erro interno: {ex.Message}");
            }
            


        }
    }
}
