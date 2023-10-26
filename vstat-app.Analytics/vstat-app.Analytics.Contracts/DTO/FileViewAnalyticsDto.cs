using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vstat_app.Analytics.Contracts.DTO
{
    public record FileViewAnalyticsDto(
    string WorkSpaceId,
    string FileId,
    string ViewerId,
    int? ViewCount,
    TimeSpan? ViewTime,
    DateTime? ViewDate);
}
