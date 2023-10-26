using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Bll.DbConfiguration;
using vstat_app.Analytics.Contracts.Commands.WorkSpaceAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Interfaces;
using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Bll.Services
{
    public class WorkSpaceAnalyticsService : IWorkSpaceAnalyticsService
    {
        private readonly AnalyticsDbContext _dbContext;

        public WorkSpaceAnalyticsService(AnalyticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WorkSpaceAnalyticsDto> CreateWorkSpaceAnalytics(Ulid ownerId, WorkSpaceAnalyticsCreateCommand workSpaceAnalyticsCommand)
        {
            var workSpaceAnalytics = workSpaceAnalyticsCommand.CreateWorkSpaceAnalytics();
            workSpaceAnalytics.OwnerId = ownerId;

            await _dbContext.WorkSpacesAnalytics.AddAsync(workSpaceAnalytics);
            await _dbContext.SaveChangesAsync();

            return new WorkSpaceAnalyticsDto(
                workSpaceAnalytics.WorkSpaceId.ToString(),
                workSpaceAnalytics.OwnerId.ToString(),
                workSpaceAnalytics.Name
            );
        }

        public async Task<WorkSpaceAnalyticsDto> GetWorkSpaceAnalyticsById(Ulid workspaceId)
        {
            var workSpaceAnalytics = await _dbContext.WorkSpacesAnalytics.AsNoTracking().FirstOrDefaultAsync(wsa => wsa.WorkSpaceId == workspaceId) ?? throw new ArgumentException("Workspace analytics not found");
            return new WorkSpaceAnalyticsDto(
                workSpaceAnalytics.WorkSpaceId.ToString(),
                workSpaceAnalytics.OwnerId.ToString(),
                workSpaceAnalytics.Name
            );
        }

        public async Task<ICollection<WorkSpaceAnalyticsDto>> GetAllWorkSpaceAnalytics()
        {
            var workSpaceAnalyticsList = await _dbContext.WorkSpacesAnalytics.AsNoTracking().ToListAsync();

            return workSpaceAnalyticsList.Select(workSpaceAnalytics => new WorkSpaceAnalyticsDto(
                workSpaceAnalytics.WorkSpaceId.ToString(),
                workSpaceAnalytics.OwnerId.ToString(),
                workSpaceAnalytics.Name
            )).ToList();
        }

        public async Task<WorkSpaceAnalyticsDto> UpdateWorkSpaceAnalytics(Ulid workspaceId, WorkSpaceAnalyticsUpdateCommand workSpaceAnalyticsCommand)
        {
            var workSpaceAnalytics = await _dbContext.WorkSpacesAnalytics.FindAsync(workspaceId) ?? throw new ArgumentException("Workspace analytics not found");
            workSpaceAnalyticsCommand.UpdateWorkSpaceAnalytics(workSpaceAnalytics);
            await _dbContext.SaveChangesAsync();

            return new WorkSpaceAnalyticsDto(
                workSpaceAnalytics.WorkSpaceId.ToString(),
                workSpaceAnalytics.OwnerId.ToString(),
                workSpaceAnalytics.Name
            );
        }

        public async Task DeleteWorkSpaceAnalytics(Ulid workspaceId)
        {
            var workSpaceAnalytics = await _dbContext.WorkSpacesAnalytics.FirstOrDefaultAsync(wsa => wsa.WorkSpaceId == workspaceId) ?? throw new ArgumentException("Workspace analytics not found");
            _dbContext.WorkSpacesAnalytics.Remove(workSpaceAnalytics);
            await _dbContext.SaveChangesAsync();
        }
    }


}
