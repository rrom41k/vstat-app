using CSharp.Ulid;

using Microsoft.AspNetCore.Mvc;

using vstat_app.Profile.Contracts.Commands.ProfileCommands;
using vstat_app.Profile.Contracts.DTO;
using vstat_app.Profile.Contracts.Interfaces;

namespace vstat_app.Profile.App.Controllers;

[ApiController]
[Route("api/profiles")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ActionResult))]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ProfileDto>>> GetAllProfiles()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        var profiles = await _profileService.GetAllProfiles(cancellationToken);

        return Ok(profiles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileDto>> GetProfileById(string id)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            var profile = await _profileService.GetProfileById(id, cancellationToken);

            return Ok(profile);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] ProfileCreateCommand profile)
    {
        var id = Ulid.NewUlid().ToString();

        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _profileService.CreateProfile(id, profile, cancellationToken);

            return CreatedAtAction(nameof(GetProfileById), new { id }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(string id, [FromBody] ProfileUpdateCommand profile)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _profileService.UpdateProfile(id, profile, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(string id)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _profileService.DeleteProfile(id, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}