namespace vstat_app.Profile.Contracts.Commands.ContactCommands;

public record ContactCreateCommand(
    string ProfileId,
    string Email);