using System.Diagnostics.CodeAnalysis;
using DataAccessLayerAbstractions;
using Domain;

namespace BusinessLayerAbstractions;

public interface IOrderService : ICrudService<OrderDto, Order>
{
    public IEnumerable<OrderDto> GetAll(string emailAddress);
    public Task<int> CancelAsync(OrderDto order);
}
