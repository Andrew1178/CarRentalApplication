using DataAccessLayer.Domain.Model;

namespace DataAccessLayer;

internal class VehicleMakeRepository : CrudRepository<VehicleMake>, IVehicleMakeRepository
{
    public VehicleMakeRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }
}
