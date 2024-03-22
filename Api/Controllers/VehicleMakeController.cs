using BusinessLayerAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class VehicleMakeController : ControllerBase
{
    private IVehicleMakeService _vehicleMakeService;
    public VehicleMakeController(IVehicleMakeService vehicleMakeService)
    {
        _vehicleMakeService = vehicleMakeService;
    }
    
 [HttpGet]
 public async Task<ActionResult<IEnumerable<VehicleMakeDto>>> Get()
 {
     return Ok(await _vehicleMakeService.GetAllAsync());
 }

[HttpGet("{id}")]
public async Task<ActionResult<VehicleMakeDto>> Get(int id)
{
    var vehicleMake = await _vehicleMakeService.GetAsync(id);

    if (vehicleMake == null)
        return NotFound();

    return Ok(vehicleMake);
}


[HttpPost] public async Task<ActionResult<VehicleMakeDto>> Create(VehicleMakeDto vehicleMake)
{
    await _vehicleMakeService.AddAsync(vehicleMake);
    return CreatedAtAction(nameof(Get), new { id = vehicleMake.Id }, vehicleMake);
}


[HttpPut("{id}")]
public async Task<IActionResult> Update(Guid id, VehicleMakeDto vehicleMake)
{
    if (id != vehicleMake.Id)
        return BadRequest();

    await _vehicleMakeService.UpdateAsync(vehicleMake);

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<ActionResult> Delete(int id)
{
    var vehicleMake = await _vehicleMakeService.GetAsync(id);
    if (vehicleMake == null)
        return NotFound();
    await _vehicleMakeService.RemoveAsync(vehicleMake);
    return NoContent();
}

}