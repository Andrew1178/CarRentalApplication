using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Domain.Model;

namespace BusinessLayer;

internal class VehicleService : CrudService<VehicleDto, Vehicle>, IVehicleService
{
    public VehicleService(IUnitOfWork unitOfWork,  IMapper mapper) : base(unitOfWork, unitOfWork.VehicleRepository, mapper)
    {
    }
}
