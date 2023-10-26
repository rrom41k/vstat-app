using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vstat_app.Analytics.Contracts.DTO
{
    public record FileAnalyticsDto(
        string Id, 
        string WorkSpaceId, 
        string OwnerId, 
        string FileName, 
        int TotalPagesCount, 
        string Type, 
        string Link);
}
