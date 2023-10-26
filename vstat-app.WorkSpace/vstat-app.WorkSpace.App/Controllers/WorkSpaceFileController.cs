using Microsoft.AspNetCore.Mvc;

using NUlid;

using vstat_app.WorkSpace.Bll;
using vstat_app.WorkSpace.Contracts.Commands.WorkSpaceFileCommands;

namespace vstat_app.WorkSpace.Controllers
{
    [Route("api/workspacefiles")]
    [ApiController]
    public class WorkSpaceFilesController : ControllerBase
    {
        private readonly WorkSpaceFileService _fileService;

        public WorkSpaceFilesController(WorkSpaceFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            var files = await _fileService.GetAllWorkSpaceFiles(cancellationToken);
            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            var file = await _fileService.GetWorkSpaceFileById(id, cancellationToken);
            if (file == null)
                return NotFound();

            return Ok(file);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkSpaceFileCreateCommand fileCreateCommand)
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            try
            {
                string id = Ulid.NewUlid().ToString();
                var createdFile = await _fileService.CreateWorkSpaceFile(id, fileCreateCommand, cancellationToken);

                return CreatedAtAction(nameof(Get), new { id = createdFile.Id }, createdFile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] WorkSpaceFileUpdateCommand fileUpdateCommand)
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            try
            {
                var updatedFile = await _fileService.UpdateWorkSpaceFile(id, fileUpdateCommand, cancellationToken);
                return Ok(updatedFile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            try
            {
                await _fileService.DeleteWorkSpaceFile(id, cancellationToken);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}