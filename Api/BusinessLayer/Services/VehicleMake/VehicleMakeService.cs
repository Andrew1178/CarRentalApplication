using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Domain.Model;

namespace BusinessLayer;

internal class VehicleMakeService : CrudService<VehicleMakeDto, VehicleMake>, IVehicleMakeService
{
    public VehicleMakeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.VehicleMakeRepository, mapper)
    {
    }
}
