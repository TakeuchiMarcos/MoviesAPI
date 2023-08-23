using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccessController : ControllerBase
{

    [HttpGet]
    //[Authorize]
    public IActionResult GetAccess()
    {
        return Ok();
    }

}
