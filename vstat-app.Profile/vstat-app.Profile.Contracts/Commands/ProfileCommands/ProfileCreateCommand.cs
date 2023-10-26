namespace vstat_app.Profile.Contracts.Commands.ProfileCommands;

public record ProfileCreateCommand(
    string Name,
    string Surname,
    string MiddleName,
    string Email,
    string PhoneNumber);