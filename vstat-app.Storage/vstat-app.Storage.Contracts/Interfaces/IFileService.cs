using vstat_app.Storage.Contracts.Commands.FileCommands;
using vstat_app.Storage.Contracts.DTO;

namespace vstat_app.Storage.Contracts.Interfaces;
using Models;

public interface IFileService
{
    Task<FileDTO> CreateFile(string id, FileCreateCommand file);
    Task<FileDTO> GetFileById(string id);
    Task<ICollection<FileDTO>> GetAllFiles();
    Task<FileDTO> UpdateFile(string id, FileUpdateCommand file);
    Task DeleteFile(string id);
}