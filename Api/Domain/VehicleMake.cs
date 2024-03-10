using System;
using System.Collections.Generic;

namespace Domain;

public partial class VehicleMake
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
