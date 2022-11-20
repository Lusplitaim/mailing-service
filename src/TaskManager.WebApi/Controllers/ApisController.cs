using Microsoft.AspNetCore.Mvc;
using TaskManager.WebApi.Data;
using TaskManager.WebApi.Models;

namespace TaskManager.WebApi.Controllers
{
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
