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
    public class GetAllOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public GetAllOrderUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> getAllOrder()
        {
            try
            {

                var listOrders = await _repository.GetAll();
                if (listOrders.Count < 1)
                {
                    return new CommandResult(true, "Não há pedidos cadastrados");
                }
                var listOrderDTO = OrderMapper.ToDtoList(listOrders);
                return new CommandResult(true, "", listOrderDTO);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, $"Erro ao pegar a lista de pedidos: {ex.Message}");
            }
        }
    }
}
