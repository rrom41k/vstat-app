using CSharp.Ulid;

using Microsoft.AspNetCore.Mvc;

using vstat_app.Profile.Contracts.Commands.ContactCommands;
using vstat_app.Profile.Contracts.DTO;
using vstat_app.Profile.Contracts.Interfaces;

namespace vstat_app.Profile.App.Controllers;

[ApiController]
[Route("api/contacts")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ActionResult))]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ContactDto>>> GetAllContacts()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        var contacts = await _contactService.GetAllContacts(cancellationToken);

        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactDto>> GetContactById(string id)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            var contact = await _contactService.GetContactById(id, cancellationToken);

            return Ok(contact);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] ContactCreateCommand contact)
    {
        var id = Ulid.NewUlid().ToString();

        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _contactService.CreateContact(id, contact, cancellationToken);

            return CreatedAtAction(nameof(GetContactById), new { id }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(string id, [FromBody] ContactUpdateCommand contact)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _contactService.UpdateContact(id, contact, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(string id)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _contactService.DeleteContact(id, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}