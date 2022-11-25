using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Application.DTO;
using TaskManager.Infrastructure.Data;
using AutoMapper;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class TasksController : BaseApiController
    {
        DatabaseContext _context;
        IMapper _mapper;
        public TasksController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetFullTasks()
        {
            var tasks = await _context.CronTaskRepository.GetFullTasks();
            var tasksDto = _mapper.Map<IEnumerable<CronTaskDto>>(tasks);
            return Ok(tasksDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetTasksByUsername()
        {
            var httpContext = ControllerContext.HttpContext;
            var username = httpContext.User.Identity?.Name;
            if (username is null) return Unauthorized("You are probably unauthorized");

            var tasks = await _context.CronTaskRepository.GetTasksByUsername(username);
            var tasksDto = _mapper.Map<IEnumerable<CronTaskDto>>(tasks);
            return Ok(tasksDto);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetTasksByUserId(int userId)
        {
            var tasks = await _context.CronTaskRepository.GetTasksByUserId(userId);
            var tasksDto = _mapper.Map<IEnumerable<CronTaskDto>>(tasks);
            return Ok(tasksDto);
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
            CronTask cronTask = _mapper.Map<CronTask>(taskDto);

            bool isCreated = await _context.CronTaskRepository.CreateTask(cronTask);

            return isCreated ? CreatedAtAction("CreateTask", null) : BadRequest("Could not create object");
        }
    }
}
