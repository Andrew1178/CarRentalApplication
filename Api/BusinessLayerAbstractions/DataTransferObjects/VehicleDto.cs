using Domain;

namespace BusinessLayerAbstractions;

public class VehicleDto 
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal UsDollarRatePerDay { get; set; }

    public int NumberAvailable { get; set; }

    public VehicleMakeDto Make { get; set; } = null!;

    public VehicleType Type { get; set; } = VehicleType.Car!;
}
