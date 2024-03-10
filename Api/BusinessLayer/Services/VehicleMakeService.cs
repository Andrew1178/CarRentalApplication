using AutoMapper;
using BusinessLayerAbstractions;
using DataAccessLayerAbstractions;
using Domain;

namespace BusinessLayer;

public class VehicleMakeService : CrudService<VehicleMakeDto, VehicleMake>, IVehicleMakeService
{
    public VehicleMakeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.VehicleMakeRepository, mapper)
    {
    }
}
