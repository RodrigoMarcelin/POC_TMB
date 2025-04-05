using Azure.Messaging.ServiceBus;
using OrderManagement.Application.UseCase;

namespace OrderManagement.API.BackgroundServices
{
    public class UpdateOrderConsumerService : BackgroundService
    {
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceScopeFactory _scopeFactory;

        public UpdateOrderConsumerService(ServiceBusClient client, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            _processor = client.CreateProcessor("order", new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync(stoppingToken);
        }

        private async Task ProcessMessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                var orderId = Guid.Parse(args.Message.Body.ToString());
                Console.WriteLine($"[ServiceBus] Mensagem recebida: {orderId}");

                // Cria um escopo para serviços scoped
                using var scope = _scopeFactory.CreateScope();
                var updateUseCase = scope.ServiceProvider.GetRequiredService<UpdateOrderStatusUseCase>();

                await updateUseCase.ExecuteAsync(orderId);

                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ServiceBus] Erro ao processar mensagem: {ex.Message}");
                await args.AbandonMessageAsync(args.Message);
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"[ServiceBus] Erro geral: {args.Exception.Message}");
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync();
            await base.StopAsync(cancellationToken);
        }
    }
}
