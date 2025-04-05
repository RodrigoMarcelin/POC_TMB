using OrderManagement.Application.DTO;
using OrderManagement.Application.Mappers;
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
    public class InsertOrderUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderNotifier _notifier;
        public InsertOrderUseCase(IOrderRepository repository, IOrderNotifier notifier)
        {
            _repository = repository;
            _notifier = notifier;
        }

        public async Task<ICommandResult> InsertOrder(InputOrderDTO command)
        {
            try
            {
                var entityOrder = new Order(command.Cliente, command.Produto, command.Valor);
                var insertOrder = await _repository.Insert(entityOrder);
                var insertOrderDTO = OrderMapper.ToDto(insertOrder);
                await _notifier.NotifyOrderUpdatedAsync();
                return new CommandResult(true, "", insertOrderDTO);

            } catch (Exception ex)
            {
                return new CommandResult(false, $"Erro inserir o Pedido: {ex.Message}");
            }
        }


    }
}
