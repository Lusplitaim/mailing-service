using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Data;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class ApisController : BaseApiController
    {
        public DatabaseContext _context;
        public ApisController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetApis()
        {
            IEnumerable<TaskApi> apis = await _context.ApiRepository.GetApis();
            return Ok(apis);
        }
    }
}
