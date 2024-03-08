using DataAccessLayer.Domain.Model;

namespace DataAccessLayer;

internal class OrderContentRepository : CrudRepository<OrderContent>, IOrderContentRepository
{
    public OrderContentRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }
}
