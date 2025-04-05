using OrderManagement.Domain.Repositories;
using OrderManagement.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.UseCase
{
    public class UpdateOrderStatusUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderNotifier _notifier;

        public UpdateOrderStatusUseCase(IOrderRepository repository, IOrderNotifier notifier)
        {
            _repository = repository;
            _notifier = notifier;
        }

        public async Task ExecuteAsync(Guid orderId)
        {
            var order = await _repository.GetById(orderId);
            if (order == null) return;

            order.Status = "Processando";
            await _repository.UpdateAsync(order);
            await _notifier.NotifyOrderUpdatedAsync();

            await Task.Delay(5000).ConfigureAwait(false);

            order.Status = "Finalizado";
            await _repository.UpdateAsync(order);
            await _notifier.NotifyOrderUpdatedAsync();
        }
    }
}
