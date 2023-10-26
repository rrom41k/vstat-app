using Microsoft.AspNetCore.Mvc;

using NUlid;

using vstat_app.WorkSpace.Bll;
using vstat_app.WorkSpace.Contracts.Commands.WorkSpaceCommands;

namespace vstat_app.WorkSpace.Controllers
{
    [Route("api/workspaces")]
    [ApiController]
    public class WorkSpaceController : ControllerBase
    {
        private readonly WorkSpaceService _workSpaceService;

        public WorkSpaceController(WorkSpaceService workSpaceService)
        {
            _workSpaceService = workSpaceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            var workSpaces = await _workSpaceService.GetAllWorkSpaces(cancellationToken);
            return Ok(workSpaces);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            var workSpace = await _workSpaceService.GetWorkSpaceById(id, cancellationToken);
            if (workSpace == null)
                return NotFound();

            return Ok(workSpace);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkSpaceCreateCommand workSpaceCreateCommand)
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            try
            {
                string id = Ulid.NewUlid().ToString();
                var createdWorkSpace = await _workSpaceService.CreateWorkSpace(id, workSpaceCreateCommand, cancellationToken);

                return CreatedAtAction(nameof(Get), new { id = createdWorkSpace.Id }, createdWorkSpace);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] WorkSpaceUpdateCommand workSpaceUpdateCommand)
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;
            try
            {
                var updatedWorkSpace = await _workSpaceService.UpdateWorkSpace(id, workSpaceUpdateCommand, cancellationToken);

                return Ok(updatedWorkSpace);
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
                await _workSpaceService.DeleteWorkSpace(id, cancellationToken);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}