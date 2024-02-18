using BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace Api;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private IVehicleService _vehicleService;
    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }
    
 [HttpGet]
 public async Task<ActionResult<IEnumerable<VehicleDto>>> Get()
 {
     return Ok(await _vehicleService.GetAllAsync());
 }

[HttpGet("{id}")]
public async Task<ActionResult<VehicleDto>> Get(int id)
{
    var vehicleMake = await _vehicleService.GetAsync(id);

    if (vehicleMake == null)
        return NotFound();

    return Ok(vehicleMake);
}

[HttpPost] async Task<ActionResult<VehicleDto>> Create(VehicleDto vehicle)
{
    await _vehicleService.AddAsync(vehicle);
    return CreatedAtAction(nameof(Get), new { id = vehicle.Id }, vehicle);
}

[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, VehicleDto vehicle)
{
    if (id != vehicle.Id)
        return BadRequest();

    await _vehicleService.UpdateAsync(vehicle);

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<ActionResult> Delete(int id)
{
    var vehicle = await _vehicleService.GetAsync(id);
    if (vehicle == null)
        return NotFound();
    await _vehicleService.RemoveAsync(vehicle);
    return NoContent();
}

}