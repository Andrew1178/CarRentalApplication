using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("[controller]/[action]")]
public class ErrorController : ControllerBase
{
[Route("/error")]
[ApiExplorerSettings(IgnoreApi = true)] // Exclude from swagger
public IActionResult Error() => Problem();   
}
