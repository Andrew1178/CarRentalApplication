using DataAccessLayerAbstractions;
using Domain;

namespace BusinessLayerAbstractions;

public interface IVehicleMakeService : ICrudService<VehicleMakeDto, VehicleMake>
{

}