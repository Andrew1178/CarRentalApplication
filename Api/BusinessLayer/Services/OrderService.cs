using System.Linq.Expressions;
using AutoMapper;
using BusinessLayerAbstractions;
using DataAccessLayerAbstractions;
using Domain;

namespace BusinessLayer;

public class OrderService : CrudService<OrderDto, Order>, IOrderService
{
    public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.OrderRepository, mapper)
    {
    }

    public async Task<int> CancelAsync(OrderDto order)
    {
        order.CancelledOn = DateTime.Now;
        return await UpdateAsync(order);
    }
}
