using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Commands.FileAnalyticsCommands
{
    public record FileAnalyticsUpdateCommand(
    string FileName,
    int TotalPagesCount,
    string? Type,
    string? Link)
    {
        public void UpdateFileAnalytics(FileAnalytics fileAnalytics)
        {
            fileAnalytics.FileName = FileName;
            fileAnalytics.TotalPagesCount = TotalPagesCount;
            fileAnalytics.Type = Type;
            fileAnalytics.Link = Link;
        }
    }
}
