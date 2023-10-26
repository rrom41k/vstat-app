using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Commands.FileAnalyticsCommands
{
    public record FileAnalyticsCreateCommand(
    string FileName,
    int TotalPagesCount,
    string? Type,
    string? Link)
    {
        public FileAnalytics CreateFileAnalytics()
        {
            return new FileAnalytics
            {
                FileName = FileName,
                TotalPagesCount = TotalPagesCount,
                Type = Type,
                Link = Link,
                FileViews = new List<FileViewAnalytics>()
            };
        }
    }
}
