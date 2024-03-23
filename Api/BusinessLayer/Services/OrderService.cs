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
        if (order.CancelledOn.HasValue)
            throw new InvalidOperationException("Order is already cancelled.");

        order.CancelledOn = DateTime.Now;
        return await UpdateAsync(order);
    }

    public IEnumerable<OrderDto> GetAll(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            throw new ArgumentNullException(nameof(emailAddress)); 

        return Find(orderDto => orderDto.EmailAddress == emailAddress);
    }
}
