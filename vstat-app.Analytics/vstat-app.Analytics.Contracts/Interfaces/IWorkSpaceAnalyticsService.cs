using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Commands.WorkSpaceAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Interfaces
{
    public interface IWorkSpaceAnalyticsService
    {
        Task<WorkSpaceAnalyticsDto> CreateWorkSpaceAnalytics(Ulid ownerId, WorkSpaceAnalyticsCreateCommand workSpaceAnalyticsCommand);
        Task<WorkSpaceAnalyticsDto> GetWorkSpaceAnalyticsById(Ulid workspaceId);
        Task<ICollection<WorkSpaceAnalyticsDto>> GetAllWorkSpaceAnalytics();
        Task<WorkSpaceAnalyticsDto> UpdateWorkSpaceAnalytics(Ulid workspaceId, WorkSpaceAnalyticsUpdateCommand workSpaceAnalyticsCommand);
        Task DeleteWorkSpaceAnalytics(Ulid workspaceId);
    }
}
