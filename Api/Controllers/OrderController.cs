using BusinessLayerAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;


/*
TODO
    1. Create order - x
    2. Get order - x
    3. Get order history - x
    4. Update order (admin only?) 
    5. Cancel order (http patch) - x
    6. Put delete in a separate repository/service. I don't want people to delete orders. Only cancel
*/  
[ApiController]
[Route("[controller]/[action]")]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
[HttpGet]
 public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
 {
     return Ok(await _orderService.GetAllAsync());
 }

[HttpGet("{id}")]
public async Task<ActionResult<VehicleDto>> Get(int id)
{
    var vehicleMake = await _orderService.GetAsync(id);

    if (vehicleMake == null)
        return NotFound();

    return Ok(vehicleMake);
}

[HttpPost] public async Task<ActionResult<OrderDto>> Create(OrderDto order)
{
    await _orderService.AddAsync(order);
    return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
}

// TODO: Make admin only
[HttpPut("{id}")]
public async Task<IActionResult> Update(Guid id, OrderDto order)
{
    if (id != order.Id)
        return BadRequest();

    await _orderService.UpdateAsync(order);

    return NoContent();
}

[HttpPatch("{id}")] 
public async Task<IActionResult> Cancel(int id)
{
    var order = await _orderService.GetAsync(id);
    if (order == null)
        return NotFound();

    await _orderService.CancelAsync(order);

    return NoContent();
}
}