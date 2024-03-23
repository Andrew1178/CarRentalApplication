using Microsoft.Extensions.Configuration;

using System.Runtime.CompilerServices;
using AutoMapper;
using BusinessLayer;

using BusinessLayerAbstractions;
using DataAccessLayer;
using DataAccessLayerAbstractions;
using Domain;
using Moq;

namespace BusinessLayerTests;

[TestClass]
public abstract class TestBase
{
    protected readonly Mock<CarRentalContext> _mockCarRentalContext;
    protected readonly Mock<IVehicleMakeRepository> _mockVehicleMakeRepository;
    protected readonly Mock<IOrderRepository> _mockOrderRepository;
    protected readonly Mock<IOrderContentRepository> _mockOrderContentRepository;
    protected readonly Mock<IVehicleRepository> _mockVehicleRepository;

    public TestBase()
    {  
        _mockCarRentalContext = new Mock<CarRentalContext>(new Mock<IConfiguration>().Object);
        _mockVehicleMakeRepository = new Mock<IVehicleMakeRepository>();
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockOrderContentRepository = new Mock<IOrderContentRepository>();
        _mockVehicleRepository = new Mock<IVehicleRepository>();
    }
}