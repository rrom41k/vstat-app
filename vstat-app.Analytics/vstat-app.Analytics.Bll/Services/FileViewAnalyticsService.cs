using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Bll.DbConfiguration;
using vstat_app.Analytics.Contracts.Commands.FileViewAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Interfaces;
using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Bll.Services
{
    public class FileViewAnalyticsService : IFileViewAnalyticsService
    {
        private readonly AnalyticsDbContext _dbContext;

        public FileViewAnalyticsService(AnalyticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FileViewAnalyticsDto> CreateFileViewAnalytics(Ulid workspaceId, Ulid fileId, Ulid viewerId, FileViewAnalyticsCreateCommand fileViewAnalyticsCommand)
        {
            var fileViewAnalytics = fileViewAnalyticsCommand.CreateFileViewAnalytics();
            fileViewAnalytics.WorkSpaceId = workspaceId;
            fileViewAnalytics.FileId = fileId;
            fileViewAnalytics.ViewerId = viewerId;

            await _dbContext.FilesViewsAnalytics.AddAsync(fileViewAnalytics);
            await _dbContext.SaveChangesAsync();

            return new FileViewAnalyticsDto(
                fileViewAnalytics.WorkSpaceId.ToString(),
                fileViewAnalytics.FileId.ToString(),
                fileViewAnalytics.ViewerId.ToString(),
                fileViewAnalytics.ViewCount,
                fileViewAnalytics.ViewTime,
                fileViewAnalytics.ViewDate
            );
        }

        public async Task<FileViewAnalyticsDto> GetFileViewAnalyticsById(Ulid workspaceId, Ulid fileId, Ulid viewerId)
        {
            var fileViewAnalytics = await _dbContext.FilesViewsAnalytics.AsNoTracking().FirstOrDefaultAsync(fva =>
                fva.WorkSpaceId == workspaceId &&
                fva.FileId == fileId &&
                fva.ViewerId == viewerId) ?? throw new ArgumentException("File view analytics not found");
            return new FileViewAnalyticsDto(
                fileViewAnalytics.WorkSpaceId.ToString(),
                fileViewAnalytics.FileId.ToString(),
                fileViewAnalytics.ViewerId.ToString(),
                fileViewAnalytics.ViewCount,
                fileViewAnalytics.ViewTime,
                fileViewAnalytics.ViewDate
            );
        }

        public async Task<ICollection<FileViewAnalyticsDto>> GetAllFileViewAnalytics()
        {
            var fileViewAnalyticsList = await _dbContext.FilesViewsAnalytics.AsNoTracking().ToListAsync();

            return fileViewAnalyticsList.Select(fileViewAnalytics => new FileViewAnalyticsDto(
                fileViewAnalytics.WorkSpaceId.ToString(),
                fileViewAnalytics.FileId.ToString(),
                fileViewAnalytics.ViewerId.ToString(),
                fileViewAnalytics.ViewCount,
                fileViewAnalytics.ViewTime,
                fileViewAnalytics.ViewDate
            )).ToList();
        }

        public async Task<FileViewAnalyticsDto> UpdateFileViewAnalytics(Ulid workspaceId, Ulid fileId, Ulid viewerId, FileViewAnalyticsUpdateCommand fileViewAnalyticsCommand)
        {
            var fileViewAnalytics = await _dbContext.FilesViewsAnalytics.FindAsync(workspaceId, fileId, viewerId) ?? throw new ArgumentException("File view analytics not found");
            fileViewAnalyticsCommand.UpdateFileViewAnalytics(fileViewAnalytics);
            await _dbContext.SaveChangesAsync();

            return new FileViewAnalyticsDto(
                fileViewAnalytics.WorkSpaceId.ToString(),
                fileViewAnalytics.FileId.ToString(),
                fileViewAnalytics.ViewerId.ToString(),
                fileViewAnalytics.ViewCount,
                fileViewAnalytics.ViewTime,
                fileViewAnalytics.ViewDate
            );
        }

        public async Task DeleteFileViewAnalytics(Ulid workspaceId, Ulid fileId, Ulid viewerId)
        {
            var fileViewAnalytics = await _dbContext.FilesViewsAnalytics.FirstOrDefaultAsync(fva =>
                fva.WorkSpaceId == workspaceId &&
                fva.FileId == fileId &&
                fva.ViewerId == viewerId) ?? throw new ArgumentException("File view analytics not found");
            _dbContext.FilesViewsAnalytics.Remove(fileViewAnalytics);
            await _dbContext.SaveChangesAsync();
        }
    }

}
