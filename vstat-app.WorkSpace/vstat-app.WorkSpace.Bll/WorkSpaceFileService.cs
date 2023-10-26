using Microsoft.EntityFrameworkCore;

using vstat_app.WorkSpace.Bll.DbConfiguration;
using vstat_app.WorkSpace.Contracts.Commands.WorkSpaceFileCommands;
using vstat_app.WorkSpace.Contracts.DTO;
using vstat_app.WorkSpace.Contracts.Interfaces;
using vstat_app.WorkSpace.Contracts.Models;

namespace vstat_app.WorkSpace.Bll
{
    public class WorkSpaceFileService : IWorkSpaceFileService
    {
        private readonly WorkSpaceDbContext _dbContext;

        public WorkSpaceFileService(WorkSpaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WorkSpaceFileDTO> CreateWorkSpaceFile(string id, WorkSpaceFileCreateCommand workSpaceFile, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            WorkSpaceFile newWorkSpaceFile = new(id, workSpaceFile.WorkSpaceId, workSpaceFile.StorageId);
            _dbContext.WorkSpaceFiles.Add(newWorkSpaceFile);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return MapToDTO(newWorkSpaceFile);
        }

        public async Task<WorkSpaceFileDTO> GetWorkSpaceFileById(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingWorkSpaceFile = await _dbContext.WorkSpaceFiles.AsNoTracking().FirstOrDefaultAsync(existingWorkSpaceFile => existingWorkSpaceFile.Id == id, cancellationToken)
                ?? throw new ArgumentException("Файл не найден.");
            return MapToDTO(existingWorkSpaceFile);
        }

        public async Task<ICollection<WorkSpaceFileDTO>> GetAllWorkSpaceFiles(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbContext.WorkSpaceFiles.AsNoTracking()
                .Select(workSpaceFile => MapToDTO(workSpaceFile))
                .ToListAsync(cancellationToken);
        }

        public async Task<WorkSpaceFileDTO> UpdateWorkSpaceFile(string id, WorkSpaceFileUpdateCommand workSpaceFile, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var workSpaceFileToUpdate = await _dbContext.WorkSpaceFiles.AsNoTracking().FirstOrDefaultAsync(workSpaceFileToUpdate => workSpaceFileToUpdate.Id == id, cancellationToken);

            if (workSpaceFileToUpdate == null)
                throw new ArgumentException("Файл не найден.");

            UpdateWorkSpaceFile(workSpaceFileToUpdate, workSpaceFile);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return MapToDTO(workSpaceFileToUpdate);
        }

        public async Task DeleteWorkSpaceFile(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingWorkSpaceFile = await _dbContext.WorkSpaces.AsNoTracking().FirstOrDefaultAsync(existingWorkSpaceFile => existingWorkSpaceFile.Id == id, cancellationToken);

            if (existingWorkSpaceFile == null)
                throw new ArgumentException("Файл не найден.");

            _dbContext.WorkSpaces.Remove(existingWorkSpaceFile);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private static WorkSpaceFileDTO MapToDTO(WorkSpaceFile workSpaceFile) => new(
            workSpaceFile.Id,
            workSpaceFile.WorkSpaceId,
            workSpaceFile.StorageId);

        public void UpdateWorkSpaceFile(WorkSpaceFile workSpaceFile, WorkSpaceFileUpdateCommand fileUpdateCommand)
        {
            workSpaceFile.WorkSpaceId = fileUpdateCommand.WorkSpaceId;
            workSpaceFile.StorageId = fileUpdateCommand.StorageId;
        }
    }
}
