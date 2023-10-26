namespace vstat_app.WorkSpace.Contracts.Commands.WorkSpaceFileCommands
{
    public record WorkSpaceFileUpdateCommand(
        string WorkSpaceId,
        string StorageId);
}