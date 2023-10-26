using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Commands.FileViewAnalyticsCommands
{
    public record FileViewAnalyticsUpdateCommand(
    int? ViewCount,
    TimeSpan? ViewTime,
    DateTime? ViewDate)
    {
        public void UpdateFileViewAnalytics(FileViewAnalytics fileViewAnalytics)
        {
            fileViewAnalytics.ViewCount = ViewCount;
            fileViewAnalytics.ViewTime = ViewTime;
            fileViewAnalytics.ViewDate = ViewDate;
        }
    }
}
