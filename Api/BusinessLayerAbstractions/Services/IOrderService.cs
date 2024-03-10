using DataAccessLayerAbstractions;
using Domain;

namespace BusinessLayerAbstractions;

public interface IOrderService : ICrudService<OrderDto, Order>
{
    public Task<int> CancelAsync(OrderDto order);
}
