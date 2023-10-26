namespace vstat_app.WorkSpace.Contracts.Commands.WorkSpaceCommands
{
    public record WorkSpaceUpdateCommand(
        string OwnerId,
        string Email,
        string Name,
        string Title,
        string CreatedAt);
}