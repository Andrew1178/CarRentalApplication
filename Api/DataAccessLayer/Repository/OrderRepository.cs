
using DataAccessLayerAbstractions;
using Domain;

namespace DataAccessLayer;

public class OrderRepository : CrudRepository<Order>, IOrderRepository
{
    public OrderRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }
}
