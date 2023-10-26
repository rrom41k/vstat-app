using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Commands.FileViewAnalyticsCommands;

namespace vstat_app.Analytics.Contracts.DTO
{
    public record CreateFileViewAnalyticsRequestDto(
        Ulid FileId,
        Ulid WorkspaceId,
        Ulid ViewerId,
        FileViewAnalyticsCreateCommand FileViewAnalyticsCommand);
}
