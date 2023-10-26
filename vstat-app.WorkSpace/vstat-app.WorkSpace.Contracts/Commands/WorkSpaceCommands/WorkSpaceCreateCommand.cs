namespace vstat_app.WorkSpace.Contracts.Commands.WorkSpaceCommands
{
    public record WorkSpaceCreateCommand(
        string OwnerId,
        string Email,
        string Name,
        string Title,
        string CreatedAt);
}