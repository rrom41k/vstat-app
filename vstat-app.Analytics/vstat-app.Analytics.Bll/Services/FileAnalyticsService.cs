using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Bll.DbConfiguration;
using vstat_app.Analytics.Contracts.Commands.FileAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Interfaces;
using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Bll.Services
{
    public class FileAnalyticsService : IFileAnalyticsService
    {
        private readonly AnalyticsDbContext _dbContext;

        public FileAnalyticsService(AnalyticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FileAnalyticsDto> CreateFileAnalytics(Ulid workSpaceId, Ulid ownerId, FileAnalyticsCreateCommand fileAnalyticsCommand)
        {
            var fileAnalytics = fileAnalyticsCommand.CreateFileAnalytics();
            fileAnalytics.WorkSpaceId = workSpaceId;
            fileAnalytics.OwnerId = ownerId;

            await _dbContext.FilesAnalytics.AddAsync(fileAnalytics);
            await _dbContext.SaveChangesAsync();
            return new FileAnalyticsDto(
                fileAnalytics.Id.ToString(),
                fileAnalytics.WorkSpaceId.ToString(),
                fileAnalytics.OwnerId.ToString(),
                fileAnalytics.FileName,
                fileAnalytics.TotalPagesCount,
                fileAnalytics.Type,
                fileAnalytics.Link
            );
        }

        public async Task<FileAnalyticsDto> GetFileAnalyticsById(Ulid id)
        {
            var fileAnalytics = await _dbContext.FilesAnalytics.AsNoTracking().FirstOrDefaultAsync(fa => fa.Id == id) ?? throw new ArgumentException("File analytics not found");
            return new FileAnalyticsDto(
                fileAnalytics.Id.ToString(),
                fileAnalytics.WorkSpaceId.ToString(),
                fileAnalytics.OwnerId.ToString(),
                fileAnalytics.FileName,
                fileAnalytics.TotalPagesCount,
                fileAnalytics.Type,
                fileAnalytics.Link
            );
        }

        public async Task<ICollection<FileAnalyticsDto>> GetAllFileAnalytics()
        {
            var fileAnalyticsList = await _dbContext.FilesAnalytics.AsNoTracking().ToListAsync();
            return fileAnalyticsList.Select(fileAnalytics => new FileAnalyticsDto(
                fileAnalytics.Id.ToString(),
                fileAnalytics.WorkSpaceId.ToString(),
                fileAnalytics.OwnerId.ToString(),
                fileAnalytics.FileName,
                fileAnalytics.TotalPagesCount,
                fileAnalytics.Type,
                fileAnalytics.Link
            )).ToList();
        }

        public async Task<FileAnalyticsDto> UpdateFileAnalytics(Ulid id, FileAnalyticsUpdateCommand fileAnalyticsCommand)
        {
            var fileAnalytics = await _dbContext.FilesAnalytics.FindAsync(id) ?? throw new ArgumentException("File analytics not found");
            fileAnalyticsCommand.UpdateFileAnalytics(fileAnalytics);

            await _dbContext.SaveChangesAsync();

            return new FileAnalyticsDto(
                fileAnalytics.Id.ToString(),
                fileAnalytics.WorkSpaceId.ToString(),
                fileAnalytics.OwnerId.ToString(),
                fileAnalytics.FileName,
                fileAnalytics.TotalPagesCount,
                fileAnalytics.Type,
                fileAnalytics.Link
            );

        }

        public async Task DeleteFileAnalytics(Ulid id)
        {
            var fileAnalytics = _dbContext.FilesAnalytics.FirstOrDefault(fa => fa.Id == id) ?? throw new ArgumentException("File analytics not found");
            _dbContext.FilesAnalytics.Remove(fileAnalytics);
            await _dbContext.SaveChangesAsync();
        }
    }
}
