using DataAccessLayer.Domain.Model;

namespace BusinessLayer;

public interface IOrderService : ICrudService<OrderDto, Order>
{
    public Task<int> CancelAsync(OrderDto order);
}
