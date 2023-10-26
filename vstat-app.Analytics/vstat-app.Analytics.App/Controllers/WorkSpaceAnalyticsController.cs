using Microsoft.AspNetCore.Mvc;

using vstat_app.Analytics.Contracts.Commands.WorkSpaceAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Interfaces;

namespace vstat_app.Analytics.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSpaceAnalyticsController : ControllerBase
    {
        private readonly IWorkSpaceAnalyticsService _workSpaceAnalyticsService;

        public WorkSpaceAnalyticsController(IWorkSpaceAnalyticsService workSpaceAnalyticsService)
        {
            _workSpaceAnalyticsService = workSpaceAnalyticsService;
        }

        // GET: api/WorkSpaceAnalytics
        [HttpGet]
        public async Task<ActionResult<ICollection<WorkSpaceAnalyticsDto>>> GetAllWorkSpaceAnalytics()
        {
            try
            {
                var workSpaceAnalytics = await _workSpaceAnalyticsService.GetAllWorkSpaceAnalytics();
                return Ok(workSpaceAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve workspace analytics. Error: " + ex.Message);
            }
        }

        // GET api/WorkSpaceAnalytics/{workspaceId}
        [HttpGet("{workspaceId}")]
        public async Task<ActionResult<WorkSpaceAnalyticsDto>> GetWorkSpaceAnalyticsById(Ulid workspaceId)
        {
            try
            {
                var workSpaceAnalytics = await _workSpaceAnalyticsService.GetWorkSpaceAnalyticsById(workspaceId);
                if (workSpaceAnalytics == null)
                    return NotFound();

                return Ok(workSpaceAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve workspace analytics by Id. Error: " + ex.Message);
            }
        }

        // POST api/WorkSpaceAnalytics
        [HttpPost]
        public async Task<IActionResult> CreateWorkSpaceAnalytics([FromBody] CreateWorkSpaceAnalyticsRequestDto request)
        {
            try
            {
                await _workSpaceAnalyticsService.CreateWorkSpaceAnalytics(request.OwnerId, request.WorkSpaceAnalyticsCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create workspace analytics. Error: " + ex.Message);
            }
        }

        // PUT api/WorkSpaceAnalytics/{workspaceId}
        [HttpPut("{workspaceId}")]
        public async Task<IActionResult> UpdateWorkSpaceAnalytics(Ulid workspaceId, [FromBody] WorkSpaceAnalyticsUpdateCommand workSpaceAnalyticsCommand)
        {
            try
            {
                await _workSpaceAnalyticsService.UpdateWorkSpaceAnalytics(workspaceId, workSpaceAnalyticsCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update workspace analytics. Error: " + ex.Message);
            }
        }

        // DELETE api/WorkSpaceAnalytics/{workspaceId}
        [HttpDelete("{workspaceId}")]
        public async Task<IActionResult> DeleteWorkSpaceAnalytics(Ulid workspaceId)
        {
            try
            {
                await _workSpaceAnalyticsService.DeleteWorkSpaceAnalytics(workspaceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete workspace analytics. Error: " + ex.Message);
            }
        }
    }
}
