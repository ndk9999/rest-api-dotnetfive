using Microsoft.AspNetCore.Mvc;

namespace BuberBreadkfast.WebApi.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}