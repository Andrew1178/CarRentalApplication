using DataAccessLayer.Domain.Model;

namespace DataAccessLayer;

internal class VehicleRepository : CrudRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }
}
