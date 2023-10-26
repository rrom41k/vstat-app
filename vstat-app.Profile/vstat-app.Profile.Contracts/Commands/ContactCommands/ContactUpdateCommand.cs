namespace vstat_app.Profile.Contracts.Commands.ContactCommands;

public record ContactUpdateCommand(
    string ProfileId,
    string Email);