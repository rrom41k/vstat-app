using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vstat_app.Analytics.Contracts.DTO
{
    public record WorkSpaceAnalyticsDto(
    string WorkSpaceId,
    string OwnerId,
    string Name);
}
