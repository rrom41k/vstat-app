using vstat_app.Storage.Contracts.Commands.StorageCommands;
using vstat_app.Storage.Contracts.DTO;

namespace vstat_app.Storage.Contracts.Interfaces;
using Models;

public interface IStorageService
{
    Task<StorageDTO> CreateStorage(string id, StorageCreateCommand storage); 
    Task<StorageDTO> GetStorageById(string id);
    Task<ICollection<StorageDTO>> GetAllStorages();
    Task<StorageDTO> UpdateStorage(string id, StorageUpdateCommand storage);
    Task DeleteStorage(string id);
}