using DataAccessLayerAbstractions;
using Domain;

namespace BusinessLayerAbstractions;

public interface IVehicleService : ICrudService<VehicleDto, Vehicle>
{

}
