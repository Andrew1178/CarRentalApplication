namespace BusinessLayer;

public class VehicleMakeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<VehicleDto> Vehicles { get; set; } = new List<VehicleDto>();
}
