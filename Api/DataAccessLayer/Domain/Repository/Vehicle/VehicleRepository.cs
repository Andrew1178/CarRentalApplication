namespace DataAccessLayer;

public class VehicleRepository : CrudRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }
}
