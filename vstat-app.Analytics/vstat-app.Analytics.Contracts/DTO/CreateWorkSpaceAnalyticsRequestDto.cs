using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Commands.WorkSpaceAnalyticsCommands;

namespace vstat_app.Analytics.Contracts.DTO
{
    public record CreateWorkSpaceAnalyticsRequestDto(
        Ulid OwnerId,
        WorkSpaceAnalyticsCreateCommand WorkSpaceAnalyticsCommand);
}
