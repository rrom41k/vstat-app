using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Contracts.Commands.WorkSpaceAnalyticsCommands
{
    public record WorkSpaceAnalyticsUpdateCommand(
    string Name)
    {
        public void UpdateWorkSpaceAnalytics(WorkSpaceAnalytics workSpaceAnalytics)
        {
            workSpaceAnalytics.Name = Name;
        }
    }
}
