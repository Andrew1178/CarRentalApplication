namespace DataAccessLayer;

public class VehicleMakeRepository : CrudRepository<VehicleMake>, IVehicleMakeRepository
{
    public VehicleMakeRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }
}
