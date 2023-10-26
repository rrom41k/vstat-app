using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Commands.FileAnalyticsCommands;
using vstat_app.Analytics.Contracts.DTO;
using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Interfaces
{
    public interface IFileAnalyticsService
    {
        Task<FileAnalyticsDto> CreateFileAnalytics(Ulid workSpaceId, Ulid ownerId, FileAnalyticsCreateCommand fileAnalyticsCommand);
        Task<FileAnalyticsDto> GetFileAnalyticsById(Ulid id);
        Task<ICollection<FileAnalyticsDto>> GetAllFileAnalytics();
        Task<FileAnalyticsDto> UpdateFileAnalytics(Ulid id, FileAnalyticsUpdateCommand fileAnalyticsCommand);
        Task DeleteFileAnalytics(Ulid id);
    }
}
