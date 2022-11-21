using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Application.DTO;
using TaskManager.Infrastructure.Data;

namespace TaskManager.WebApi.Controllers
{
    //[Authorize]
    public class TasksController : BaseApiController
    {
        DatabaseContext _context;
        public TasksController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetFullTasks()
        {
            var tasks = await _context.CronTaskRepository.GetFullTasks();
            return Ok(tasks);
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
