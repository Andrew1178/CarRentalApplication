using Microsoft.AspNetCore.Mvc;

namespace Api;

public class ErrorController : ControllerBase
{
[Route("/error")]
[ApiExplorerSettings(IgnoreApi = true)] // Exclude from swagger
public IActionResult Error() => Problem();   
}
