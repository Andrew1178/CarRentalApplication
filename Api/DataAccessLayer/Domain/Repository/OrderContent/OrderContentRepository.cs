namespace DataAccessLayer;

public class OrderContentRepository : CrudRepository<OrderContent>, IOrderContentRepository
{
    public OrderContentRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }
}
