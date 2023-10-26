using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Commands.WorkSpaceAnalyticsCommands
{
    public record WorkSpaceAnalyticsCreateCommand(
    string Name)
    {
        public WorkSpaceAnalytics CreateWorkSpaceAnalytics()
        {
            return new WorkSpaceAnalytics
            {
                Name = Name,
            };
        }
    }
}
