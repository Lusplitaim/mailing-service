using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.WebApi.DTO;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class TasksController : BaseApiController
    {
        [HttpPost]
        public ActionResult CreateTask([FromBody] CronTaskDto taskDto)
        {
            HttpContext context = ControllerContext.HttpContext;
            var user = context.User;
            return Ok(user);
        }
    }
}
