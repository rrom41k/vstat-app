using vstat_app.Storage.Contracts.DTO;
using File = vstat_app.Storage.Contracts.Models.File;

namespace vstat_app.Storage.Contracts.Commands.FileCommands;

public record FileUpdateCommand(
    string OwnerId,
    string Name,
    string Extension,
    string BucketId,
    string StorageId);