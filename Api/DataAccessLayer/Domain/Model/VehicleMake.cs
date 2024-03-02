using System;
using System.Collections.Generic;

namespace DataAccessLayer.Domain.Model;

public partial class VehicleMake
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
