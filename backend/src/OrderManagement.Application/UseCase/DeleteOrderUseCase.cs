using OrderManagement.Application.DTO;
using OrderManagement.Domain.Entities;
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
    public class DeleteOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderUseCase(IOrderRepository orderRepository)
        {
            _repository = orderRepository;
        }

        public async Task<ICommandResult> DeleteOrder(Guid deleteId)
        {
            try
            {
                
                var deleteOrder = await _repository.DeleteById(deleteId);

                if (deleteOrder != false)
                {
                    return new CommandResult(true, "Pedido deletado com sucesso");
                } 
                else
                {
                    return new CommandResult(false, "Erro ao deletar pedido");
                }
                
            } catch (Exception ex)
            {
                return new CommandResult(false, $"Erro ao deletar: {ex.Message}");
            }
        }
    }
}
