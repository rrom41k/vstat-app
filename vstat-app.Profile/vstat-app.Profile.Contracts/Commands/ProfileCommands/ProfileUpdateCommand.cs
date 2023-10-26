namespace vstat_app.Profile.Contracts.Commands.ProfileCommands;

public record ProfileUpdateCommand(
    string Name,
    string Surname,
    string MiddleName,
    string Email,
    string PhoneNumber);