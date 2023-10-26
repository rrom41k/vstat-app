using Microsoft.EntityFrameworkCore;

using vstat_app.WorkSpace.Bll.DbConfiguration;
using vstat_app.WorkSpace.Contracts.Commands.WorkSpaceCommands;
using vstat_app.WorkSpace.Contracts.DTO;
using vstat_app.WorkSpace.Contracts.Interfaces;

using ModelWorkSpace = vstat_app.WorkSpace.Contracts.Models.WorkSpace;

namespace vstat_app.WorkSpace.Bll;

public class WorkSpaceService : IWorkSpaceService
{
    private readonly WorkSpaceDbContext _dbContext;

    public WorkSpaceService(WorkSpaceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<WorkSpaceDTO> CreateWorkSpace(string id, WorkSpaceCreateCommand workSpace, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        ModelWorkSpace newWorkSpace = new(
            id,
            workSpace.OwnerId,
            workSpace.Email,
            workSpace.Name,
            workSpace.Title,
            workSpace.CreatedAt);
        _dbContext.WorkSpaces.Add(newWorkSpace);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToDTO(newWorkSpace);
    }

    public async Task<WorkSpaceDTO> GetWorkSpaceById(string id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var existingWorkSpace = await _dbContext.WorkSpaces.AsNoTracking().FirstOrDefaultAsync(existingWorkSpace => existingWorkSpace.Id == id, cancellationToken)
            ?? throw new ArgumentException("Рабочее пространство не найдено.");
        return MapToDTO(existingWorkSpace);
    }

    public async Task<ICollection<WorkSpaceDTO>> GetAllWorkSpaces(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _dbContext.WorkSpaces.AsNoTracking().Select(workSpace => MapToDTO(workSpace)).ToListAsync(cancellationToken);
    }

    public async Task<WorkSpaceDTO> UpdateWorkSpace(string id, WorkSpaceUpdateCommand workSpace, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var workSpaceToUpdate = await _dbContext.WorkSpaces.AsNoTracking().FirstOrDefaultAsync(workSpaceToUpdate => workSpaceToUpdate.Id == id, cancellationToken);

        if (workSpaceToUpdate == null)
            throw new ArgumentException("Рабочее пространство не найдено.");

        UpdateWorkSpace(workSpaceToUpdate, workSpace);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToDTO(workSpaceToUpdate);
    }

    public async Task DeleteWorkSpace(string id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var existingWorkSpace = await _dbContext.WorkSpaces.AsNoTracking().FirstOrDefaultAsync(existingWorkSpace => existingWorkSpace.Id == id, cancellationToken);

        if (existingWorkSpace == null)
            throw new ArgumentException("Рабочее пространство не найдено.");

        _dbContext.WorkSpaces.Remove(existingWorkSpace);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static WorkSpaceDTO MapToDTO(ModelWorkSpace workSpace) => new(
        workSpace.Id,
        workSpace.OwnerId,
        workSpace.Email,
        workSpace.Name,
        workSpace.Title,
        workSpace.CreatedAt);

    private void UpdateWorkSpace(ModelWorkSpace workSpace, WorkSpaceUpdateCommand workSpaceUpdateCommand)
    {
        workSpace.OwnerId = workSpaceUpdateCommand.OwnerId;
        workSpace.Email = workSpaceUpdateCommand.Email;
        workSpace.Name = workSpaceUpdateCommand.Name;
        workSpace.Title = workSpaceUpdateCommand.Title;
        workSpace.CreatedAt = workSpaceUpdateCommand.CreatedAt;
    }
}