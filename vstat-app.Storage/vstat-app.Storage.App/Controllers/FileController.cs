using NUlid;
using Microsoft.AspNetCore.Mvc;
using vstat_app.Storage.Bll;
using vstat_app.Storage.Contracts.Commands.FileCommands;

namespace vstat_app.Storage.App.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileService _fileService;

        public FilesController(FileService fileService)
        {
            _fileService = fileService;
        }

        // GET: api/files
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var files = await _fileService.GetAllFiles();
            return Ok(files);
        }

        // GET: api/files/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var file = await _fileService.GetFileById(id);
            if (file == null)
                return NotFound();

            return Ok(file);
        }

        // POST: api/files
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FileCreateCommand fileCreateCommand)
        {
            try
            {
                string id = Ulid.NewUlid().ToString();
                var createdFile = await _fileService.CreateFile(id, fileCreateCommand);
                return CreatedAtAction(nameof(Get), new { id = createdFile.Id }, createdFile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/files/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] FileUpdateCommand fileUpdateCommand)
        {
            try
            {
                var updatedFile = await _fileService.UpdateFile(id, fileUpdateCommand);
                return Ok(updatedFile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/files/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _fileService.DeleteFile(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
