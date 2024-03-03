using System;
using System.Collections.Generic;

namespace DataAccessLayer.Domain.Model;

public partial class Vehicle
{
    public Guid Id { get; set; }

    public int TypeId { get; set; }

    public Guid MakeId { get; set; }

    public string Name { get; set; } = null!;

    public decimal UsDollarRatePerDay { get; set; }

    public int NumberAvailable { get; set; }

    public virtual VehicleMake Make { get; set; } = null!;

    public virtual ICollection<OrderContent> OrderContents { get; set; } = new List<OrderContent>();
}
