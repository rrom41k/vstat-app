namespace vstat_app.WorkSpace.Contracts.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

using vstat_app.WorkSpace.Contracts.Commands.WorkSpaceCommands;
using vstat_app.WorkSpace.Contracts.DTO;

public interface IWorkSpaceService
{
    Task<WorkSpaceDTO> CreateWorkSpace(string id, WorkSpaceCreateCommand workSpace, CancellationToken cancellationToken = default);
    Task<WorkSpaceDTO> GetWorkSpaceById(string id, CancellationToken cancellationToken = default);
    Task<ICollection<WorkSpaceDTO>> GetAllWorkSpaces(CancellationToken cancellationToken = default);
    Task<WorkSpaceDTO> UpdateWorkSpace(string id, WorkSpaceUpdateCommand workSpace, CancellationToken cancellationToken = default);
    Task DeleteWorkSpace(string id, CancellationToken cancellationToken = default);
}