using vstat_app.Profile.Contracts.Commands.ProfileCommands;
using vstat_app.Profile.Contracts.DTO;

namespace vstat_app.Profile.Contracts.Interfaces;

public interface IProfileService
{
    Task<ProfileDto> CreateProfile(
        string id,
        ProfileCreateCommand profile,
        CancellationToken cancellationToken = default);

    Task<ProfileDto> GetProfileById(string id, CancellationToken cancellationToken = default);
    Task<ICollection<ProfileDto>> GetAllProfiles(CancellationToken cancellationToken = default);

    Task<ProfileDto> UpdateProfile(
        string id,
        ProfileUpdateCommand profile,
        CancellationToken cancellationToken = default);

    Task DeleteProfile(string id, CancellationToken cancellationToken = default);
}