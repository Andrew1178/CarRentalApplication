﻿using AutoMapper;
using DataAccessLayer;

namespace BusinessLayer;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Vehicle, VehicleDto>();
        CreateMap<VehicleDto, Vehicle>();
        CreateMap<VehicleMake, VehicleMakeDto>();
        CreateMap<VehicleMakeDto, VehicleMake>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();
    }
}
