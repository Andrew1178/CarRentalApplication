using DataAccessLayer;

namespace BusinessLayer;

public class VehicleDto 
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal UsDollarRatePerDay { get; set; }

    public int NumberAvailable { get; set; }

    public VehicleMakeDto Make { get; set; } = null!;

    public VehicleType Type { get; set; } = VehicleType.Car!;
}
