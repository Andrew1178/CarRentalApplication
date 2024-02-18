﻿using AutoMapper;
using DataAccessLayer;

namespace BusinessLayer;

public class VehicleService : CrudService<VehicleDto, Vehicle>, IVehicleService
{
    public VehicleService(IUnitOfWork unitOfWork,  IMapper mapper) : base(unitOfWork, unitOfWork.VehicleRepository, mapper)
    {
    }
}
