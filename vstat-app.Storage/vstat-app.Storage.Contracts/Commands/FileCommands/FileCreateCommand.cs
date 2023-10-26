using  vstat_app.Storage.Contracts.DTO;

namespace vstat_app.Storage.Contracts.Commands.FileCommands;

public record FileCreateCommand(string OwnerId, string Name, string Extension, string BucketId, string StorageId);