using NUlid;
using Microsoft.AspNetCore.Mvc;
using vstat_app.Storage.Contracts.Commands.StorageCommands;
using vstat_app.Storage.Contracts.Interfaces;

namespace vstat_app.WorkSpace.Storage.Controllers;

[Route("api/storages")]
[ApiController]
public class StorageController : ControllerBase
{
    private readonly IStorageService _storageService;

    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }
    
    // GET: api/storages
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var storages = await _storageService.GetAllStorages();
        return Ok(storages);
    }

    // GET: api/storages/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var storage = await _storageService.GetStorageById(id);
        if (storage == null)
            return NotFound();

        return Ok(storage);
    }

    // POST: api/storages
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StorageCreateCommand storageCreateCommand)
    {
        try
        {
            string id = Ulid.NewUlid().ToString();
            var createdStorage = await _storageService.CreateStorage(id, storageCreateCommand);
            return CreatedAtAction(nameof(Get), new { id = createdStorage.Id }, createdStorage);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/storages/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] StorageUpdateCommand storageUpdateCommand)
    {
        try
        {
            var updatedStorage = await _storageService.UpdateStorage(id, storageUpdateCommand);
            return Ok(updatedStorage);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/strages/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _storageService.DeleteStorage(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
