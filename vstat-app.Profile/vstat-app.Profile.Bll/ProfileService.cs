using Microsoft.EntityFrameworkCore;

using vstat_app.Profile.Bll.DbConfiguration;
using vstat_app.Profile.Contracts.Commands.ProfileCommands;
using vstat_app.Profile.Contracts.DTO;
using vstat_app.Profile.Contracts.Interfaces;

namespace vstat_app.Profile.Bll;

public class ProfileService : IProfileService
{
    private readonly ProfileDbContext _dbContext;

    public ProfileService(ProfileDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProfileDto> CreateProfile(
        string id,
        ProfileCreateCommand profile,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _dbContext.Profiles.AddAsync(
            new(
                id,
                profile.Name,
                profile.Surname,
                profile.MiddleName,
                profile.Email,
                profile.PhoneNumber),
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(
            id,
            profile.Name,
            profile.Surname,
            profile.MiddleName,
            profile.Email,
            profile.PhoneNumber);
    }

    public async Task<ProfileDto> GetProfileById(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var profile = await _dbContext.Profiles.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (profile == null)
        {
            throw new ArgumentException("Профиль не найден.");
        }

        return new(
            profile.Id,
            profile.Name,
            profile.Surname,
            profile.MiddleName,
            profile.Email,
            profile.PhoneNumber);
    }

    public async Task<ICollection<ProfileDto>> GetAllProfiles(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var profiles = await _dbContext.Profiles.AsNoTracking().ToListAsync(cancellationToken);
        var profilesDto = profiles.Select(
                c => new ProfileDto(
                    c.Id,
                    c.Name,
                    c.Surname,
                    c.MiddleName,
                    c.Email,
                    c.PhoneNumber))
            .ToList();

        return profilesDto;
    }

    public async Task<ProfileDto> UpdateProfile(
        string id,
        ProfileUpdateCommand profile,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existingProfile = _dbContext.Profiles.FirstOrDefault(p => p.Id == id);

        if (existingProfile == null)
        {
            throw new ArgumentException("Профиль не найден.");
        }

        existingProfile.Name = profile.Name;
        existingProfile.Surname = profile.Surname;
        existingProfile.MiddleName = profile.MiddleName;
        existingProfile.Email = profile.Email;
        existingProfile.PhoneNumber = profile.PhoneNumber;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(
            id,
            profile.Name,
            profile.Surname,
            profile.MiddleName,
            profile.Email,
            profile.PhoneNumber);
    }

    public async Task DeleteProfile(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var profileToDelete = _dbContext.Profiles.FirstOrDefault(p => p.Id == id);

        if (profileToDelete == null)
        {
            throw new ArgumentException("Профиль не найден.");
        }

        _dbContext.Profiles.Remove(profileToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}