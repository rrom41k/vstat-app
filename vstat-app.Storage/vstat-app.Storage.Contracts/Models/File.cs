namespace vstat_app.Storage.Contracts.Models;

public class File
{
    public File(string id, string ownerId, string name,
        string extension,
        string bucketId,
        string storageId)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
        Extension = extension;
        BucketId = bucketId;
        StorageId = storageId;
    }

    public string Id { get; set; }
    public string OwnerId { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public string BucketId { get; set; }
    public string StorageId { get; set; }
    public  Storage? Storage { get; set; }
}