using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design.Serialization;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccessController : ControllerBase
{


    public AccessController(IAuthorizationMiddlewareResultHandler authorizationHandler)
    {

    }

    [HttpGet]
    [Authorize(Policy = "MinimunAge")]
    public IActionResult GetAccess()
    {
        return Ok("Access Granted");
    }

}
