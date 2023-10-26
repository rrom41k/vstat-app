using Microsoft.AspNetCore.Mvc;

using vstat_app.Analytics.Contracts.Commands.FileAnalyticsCommands;
using vstat_app.Analytics.Contracts.Commands.FileViewAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Interfaces;

namespace vstat_app.Analytics.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileAnalyticsController : ControllerBase
    {
        private readonly IFileAnalyticsService _fileAnalyticsService;

        public FileAnalyticsController(IFileAnalyticsService fileAnalyticsService)
        {
            _fileAnalyticsService = fileAnalyticsService;
        }

        // GET: api/FileAnalytics
        [HttpGet]
        public async Task<ActionResult<ICollection<FileAnalyticsDto>>> GetAllFileAnalytics()
        {
            try
            {
                var fileAnalytics = await _fileAnalyticsService.GetAllFileAnalytics();
                return Ok(fileAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve file analytics. Error: " + ex.Message);
            }
        }

        // GET api/FileAnalytics/id
        [HttpGet("{id}")]
        public async Task<ActionResult<FileAnalyticsDto>> GetFileAnalyticsById(Ulid id)
        {
            try
            {
                var fileAnalytics = await _fileAnalyticsService.GetFileAnalyticsById(id);
                if (fileAnalytics == null)
                    return NotFound();

                return Ok(fileAnalytics);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve file analytics by Id. Error: " + ex.Message);
            }
        }

        // POST api/FileAnalytics
        [HttpPost]
        public async Task<IActionResult> CreateFileAnalytics([FromBody] CreateFileAnalyticsRequestDto request)
        {
            try
            {
                var fileAnalyticsCommand = request.FileAnalyticsCommand;
                await _fileAnalyticsService.CreateFileAnalytics(request.WorkspaceId, request.OwnerId, fileAnalyticsCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create file analytics. Error: " + ex.Message);
            }
        }

        // PUT api/FileAnalytics/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFileAnalytics(Ulid id, [FromBody] FileAnalyticsUpdateCommand fileAnalyticsCommand)
        {
            try
            {
                await _fileAnalyticsService.UpdateFileAnalytics(id, fileAnalyticsCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update file analytics. Error: " + ex.Message);
            }
        }

        // DELETE api/FileAnalytics/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileAnalytics(Ulid id)
        {
            try
            {
                await _fileAnalyticsService.DeleteFileAnalytics(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete file analytics. Error: " + ex.Message);
            }
        }
    }
}
