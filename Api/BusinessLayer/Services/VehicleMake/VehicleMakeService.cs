using AutoMapper;
using DataAccessLayer;

namespace BusinessLayer;

public class VehicleMakeService : CrudService<VehicleMakeDto, VehicleMake>, IVehicleMakeService
{
    public VehicleMakeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.VehicleMakeRepository, mapper)
    {
    }
}
