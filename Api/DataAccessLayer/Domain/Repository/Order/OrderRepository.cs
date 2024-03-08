﻿using DataAccessLayer.Domain.Model;

namespace DataAccessLayer;

internal class OrderRepository : CrudRepository<Order>, IOrderRepository
{
    public OrderRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }
}
