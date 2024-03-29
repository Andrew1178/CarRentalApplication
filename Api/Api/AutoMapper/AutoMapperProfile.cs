﻿using AutoMapper;
using BusinessLayerAbstractions;
using DataAccessLayerAbstractions;
using Domain;

namespace Api;

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
