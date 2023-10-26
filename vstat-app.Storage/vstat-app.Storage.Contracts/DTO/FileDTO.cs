namespace vstat_app.Storage.Contracts.DTO;

public record FileDTO(string Id, string OwnerId, string Name, string Extension, string BucketId, string StorageId);