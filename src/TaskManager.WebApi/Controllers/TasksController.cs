using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Application.DTO;
using TaskManager.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

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

        [HttpGet]
        public async Task<ActionResult> GetFullTasks()
        {
            var tasks = await _context.CronTaskRepository.GetFullTasks();
            return Ok(tasks);
        }

        [HttpGet]
        public async Task<ActionResult> GetTasksByUsername()
        {
            var httpContext = ControllerContext.HttpContext;
            var username = httpContext.User.Identity?.Name;
            bool isInRole = httpContext.User.IsInRole("member");
            if (username is null) return Unauthorized("You are probably unauthorized");

            var tasks = await _context.CronTaskRepository.GetTasksByUsername(username);
            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            bool isDeleted = await _context.CronTaskRepository.DeleteTask(id);
            return Ok(isDeleted);
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
                UrlParamsString = taskDto.UrlParamsString,
                UserId = taskDto.UserId,
                ApiId = taskDto.ApiId,
            };

            bool isCreated = await _context.CronTaskRepository.CreateTask(cronTask);

            return isCreated ? Ok() : BadRequest("Could not create object");
        }
    }
}
