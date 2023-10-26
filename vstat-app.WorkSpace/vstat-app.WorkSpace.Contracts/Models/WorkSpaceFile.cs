namespace vstat_app.WorkSpace.Contracts.Models;

public class WorkSpaceFile
{
    public WorkSpaceFile(string id, string workSpaceId, string storageId)
    {
        Id = id;
        WorkSpaceId = workSpaceId;
        StorageId = storageId;
    }

    public string Id { get; set; }
    public string WorkSpaceId { get; set; }
    public string StorageId { get; set; }
    public WorkSpace? WorkSpace { get; set; }
}