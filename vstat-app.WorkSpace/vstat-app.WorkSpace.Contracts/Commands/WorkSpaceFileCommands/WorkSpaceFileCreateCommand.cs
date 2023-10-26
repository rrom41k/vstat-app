namespace vstat_app.WorkSpace.Contracts.Commands.WorkSpaceFileCommands
{
    public record WorkSpaceFileCreateCommand(
        string WorkSpaceId,
        string StorageId);
}