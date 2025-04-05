using Microsoft.AspNetCore.SignalR;
using OrderManagement.API.Hubs;
using OrderManagement.Shared.Interface;

namespace OrderManagement.API.Services
{
    public class SignalROrderNotifier : IOrderNotifier
    {

        private readonly IHubContext<OrderHub> _hubContext;

        public SignalROrderNotifier(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task NotifyOrderUpdatedAsync()
        {
            await _hubContext.Clients.All.SendAsync("OrderUpdated");
        }
    }
}
