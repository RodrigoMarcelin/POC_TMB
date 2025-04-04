using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.UseCase;
using OrderManagement.Shared.Commands;
using OrderManagement.Shared.Interface;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly InsertOrderUseCase _insertOrderUseCase;
        private readonly UpdateOrderUseCase _updateOrderUseCase;
        private readonly GetOrderByIdUseCase _getOrderByIdUseCase;
        private readonly GetAllOrderUseCase _getAllOrderUseCase;
        private readonly DeleteOrderUseCase _deleteOrderUseCase; 

        public OrdersController(
            InsertOrderUseCase insertOrderUseCase,
            UpdateOrderUseCase updateOrderUseCase,
            GetOrderByIdUseCase getOrderByIdUseCase,
            GetAllOrderUseCase getAllOrderUseCase,
            DeleteOrderUseCase deleteOrderUseCase)  
        {
            _insertOrderUseCase = insertOrderUseCase;
            _updateOrderUseCase = updateOrderUseCase;
            _getOrderByIdUseCase = getOrderByIdUseCase;
            _getAllOrderUseCase = getAllOrderUseCase;
            _deleteOrderUseCase = deleteOrderUseCase;
        }

        // POST api/orders
        [HttpPost]
        public async Task<ICommandResult> Create([FromBody] InputOrderDTO orderDto)
        {
            return (CommandResult)await _insertOrderUseCase.InsertOrder(orderDto);
        }

        // PUT api/orders
        [HttpPut("{id}")]
        public async Task<ICommandResult> Update(Guid id, [FromBody] OrderDTO orderDto)
        {
            return (CommandResult)await _updateOrderUseCase.UpdateOrder(id, orderDto);

        }

        // GET api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ICommandResult> GetById(Guid id)
        {
            return (CommandResult)await _getOrderByIdUseCase.GetOrderById(id);
        }

        // GET api/orders
        [HttpGet]
        public async Task<ICommandResult> GetAll()
        {
            return (CommandResult)await _getAllOrderUseCase.getAllOrder();
        }

        // DELETE api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<ICommandResult> Delete(Guid id)
        {
            return (CommandResult)await _deleteOrderUseCase.DeleteOrder(id);
        }
    }
}
