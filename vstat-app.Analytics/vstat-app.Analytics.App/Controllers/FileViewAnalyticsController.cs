using Microsoft.AspNetCore.Mvc;

using vstat_app.Analytics.Contracts.Commands.FileViewAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Interfaces;

namespace vstat_app.Analytics.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileViewAnalyticsController : ControllerBase
    {
        private readonly IFileViewAnalyticsService _fileViewAnalyticsService;

        public FileViewAnalyticsController(IFileViewAnalyticsService fileViewAnalyticsService)
        {
            _fileViewAnalyticsService = fileViewAnalyticsService;
        }

        // GET: api/FileViewAnalytics
        [HttpGet]
        public async Task<ActionResult<ICollection<FileViewAnalyticsDto>>> GetAllFileViewAnalytics()
        {
            try
            {
                var fileViewAnalytics = await _fileViewAnalyticsService.GetAllFileViewAnalytics();
                return Ok(fileViewAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve file view analytics. Error: " + ex.Message);
            }
        }

        // GET api/FileViewAnalytics/{workspaceId}/{fileId}/{viewerId}
        [HttpGet("{workspaceId}/{fileId}/{viewerId}")]
        public async Task<ActionResult<FileViewAnalyticsDto>> GetFileViewAnalyticsById(Ulid workspaceId, Ulid fileId, Ulid viewerId)
        {
            try
            {
                var fileViewAnalytics = await _fileViewAnalyticsService.GetFileViewAnalyticsById(workspaceId, fileId, viewerId);
                if (fileViewAnalytics == null)
                    return NotFound();

                return Ok(fileViewAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve file view analytics by Id. Error: " + ex.Message);
            }
        }

        // POST api/FileViewAnalytics
        [HttpPost]
        public async Task<IActionResult> CreateFileViewAnalytics([FromBody] CreateFileViewAnalyticsRequestDto request)
        {
            try
            {
                await _fileViewAnalyticsService.CreateFileViewAnalytics(request.WorkspaceId, request.FileId, request.ViewerId, request.FileViewAnalyticsCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create file view analytics. Error: " + ex.Message);
            }
        }

        // PUT api/FileViewAnalytics/{workspaceId}/{fileId}/{viewerId}
        [HttpPut("{workspaceId}/{fileId}/{viewerId}")]
        public async Task<IActionResult> UpdateFileViewAnalytics(Ulid workspaceId, Ulid fileId, Ulid viewerId, [FromBody] FileViewAnalyticsUpdateCommand fileViewAnalyticsCommand)
        {
            try
            {
                await _fileViewAnalyticsService.UpdateFileViewAnalytics(workspaceId, fileId, viewerId, fileViewAnalyticsCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update file view analytics. Error: " + ex.Message);
            }
        }

        // DELETE api/FileViewAnalytics/{workspaceId}/{fileId}/{viewerId}
        [HttpDelete("{workspaceId}/{fileId}/{viewerId}")]
        public async Task<IActionResult> DeleteFileViewAnalytics(Ulid workspaceId, Ulid fileId, Ulid viewerId)
        {
            try
            {
                await _fileViewAnalyticsService.DeleteFileViewAnalytics(workspaceId, fileId, viewerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete file view analytics. Error: " + ex.Message);
            }
        }
    }
}
