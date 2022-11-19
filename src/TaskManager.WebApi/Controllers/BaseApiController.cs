using Microsoft.AspNetCore.Mvc;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BaseApiController : ControllerBase
{
    public BaseApiController()
    {
    }
}
