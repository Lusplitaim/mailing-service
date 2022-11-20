using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.WebApi.Data;
using TaskManager.WebApi.DTO;
using TaskManager.WebApi.Models;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class TasksController : BaseApiController
    {
        DatabaseContext _context;
        public TasksController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] CronTaskDto taskDto)
        {
            CronTask cronTask = new()
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                Minutes = taskDto.Minutes,
                Hours = taskDto.Hours,
                Days = taskDto.Days,
                Months = taskDto.Months,
                Weekdays = taskDto.Weekdays,
                UserId = taskDto.UserId,
                ApiId = taskDto.ApiId,
            };

            bool isCreated = await _context.CronTaskRepository.CreateTask(cronTask);

            return isCreated ? Ok() : BadRequest("Could not create object");
        }
    }
}
