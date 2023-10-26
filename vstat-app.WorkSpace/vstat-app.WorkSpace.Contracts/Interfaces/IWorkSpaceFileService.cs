namespace vstat_app.WorkSpace.Contracts.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

using vstat_app.WorkSpace.Contracts.Commands.WorkSpaceFileCommands;
using vstat_app.WorkSpace.Contracts.DTO;

public interface IWorkSpaceFileService
{
    Task<WorkSpaceFileDTO> CreateWorkSpaceFile(string id, WorkSpaceFileCreateCommand workSpaceFile, CancellationToken cancellationToken = default);
    Task<WorkSpaceFileDTO> GetWorkSpaceFileById(string id, CancellationToken cancellationToken = default);
    Task<ICollection<WorkSpaceFileDTO>> GetAllWorkSpaceFiles(CancellationToken cancellationToken = default);
    Task<WorkSpaceFileDTO> UpdateWorkSpaceFile(string id, WorkSpaceFileUpdateCommand workSpaceFile, CancellationToken cancellationToken = default);
    Task DeleteWorkSpaceFile(string id, CancellationToken cancellationToken = default);
}