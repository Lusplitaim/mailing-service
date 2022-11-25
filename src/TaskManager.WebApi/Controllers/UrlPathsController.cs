using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Infrastructure.Data;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class UrlPathsController : BaseApiController
    {
        public DatabaseContext _context;
        public UrlPathsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("{apiId}")]
        public async Task<ActionResult> GetPathsByApiId(int apiId)
        {
            var paths = await _context.UrlPathRepository.GetPathsByApi(apiId);
            return Ok(paths);
        }

        [HttpGet("{pathId}")]
        public async Task<ActionResult> GetPathParamsById(int pathId)
        {
            var urlParams = await _context.UrlParamRepository.GetParamsByPath(pathId);
            return Ok(urlParams);
        }
    }
}
