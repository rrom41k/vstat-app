using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Commands.FileViewAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Interfaces
{
    public interface IFileViewAnalyticsService
    {
        Task<FileViewAnalyticsDto> CreateFileViewAnalytics(Ulid workspaceId, Ulid fileId, Ulid viewerId, FileViewAnalyticsCreateCommand fileViewAnalyticsCommand);
        Task<FileViewAnalyticsDto> GetFileViewAnalyticsById(Ulid workspaceId, Ulid fileId, Ulid viewerId);
        Task<ICollection<FileViewAnalyticsDto>> GetAllFileViewAnalytics();
        Task<FileViewAnalyticsDto> UpdateFileViewAnalytics(Ulid workspaceId, Ulid fileId, Ulid viewerId, FileViewAnalyticsUpdateCommand fileViewAnalyticsCommand);
        Task DeleteFileViewAnalytics(Ulid workspaceId, Ulid fileId, Ulid viewerId);
    }
}
