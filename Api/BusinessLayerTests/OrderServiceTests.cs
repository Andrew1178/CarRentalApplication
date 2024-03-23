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
public class OrderServiceTests : TestBase
{
    // Method so individual tests can setup the mocks however they want before creating the servive.
    private OrderService GetOrderService()
    {
        return new OrderService(new UnitOfWork(_mockCarRentalContext.Object, _mockVehicleMakeRepository.Object, _mockOrderRepository.Object, _mockOrderContentRepository.Object, _mockVehicleRepository.Object), new Mock<IMapper>().Object);
    }

    // Naming convention followed: MethodName_StateUnderTest_ExpectedBehavior   
    [TestMethod]
    public async Task CancelAsync_OrderAlreadyCancelled_InvalidOperationExceptionThrown()
    {
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => GetOrderService().CancelAsync(new OrderDto { CancelledOn = DateTime.Now }));
    }

    [TestMethod]
    public async Task CancelAsync_OpenOrder_OrderCancelled()
    {
        var order = new OrderDto();
        await GetOrderService().CancelAsync(order);
        Assert.IsNotNull(order.CancelledOn);
    }
}