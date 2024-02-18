using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial record OrderContent
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int VehicleId { get; set; }

    public decimal VehicleUsDollarRatePerDay { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
