namespace vstat_app.Profile.Contracts.DTO;

public record ProfileDto(
    string Id,
    string Name,
    string Surname,
    string MiddleName,
    string Email,
    string PhoneNumber);