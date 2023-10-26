using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Commands.FileViewAnalyticsCommands
{
    public record FileViewAnalyticsCreateCommand(
    int? ViewCount,
    TimeSpan? ViewTime,
    DateTime? ViewDate)
    {
        public FileViewAnalytics CreateFileViewAnalytics()
        {
            return new FileViewAnalytics
            {
                ViewCount = ViewCount,
                ViewTime = ViewTime,
                ViewDate = ViewDate
            };
        }
    }
}
