using Microsoft.AspNetCore.Mvc;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BaseApiController : ControllerBase
{
    protected readonly ILogger<BaseApiController> _logger;

    public BaseApiController(ILogger<BaseApiController> logger)
    {
        _logger = logger;
    }
}
