using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vstat_app.Analytics.Contracts.Models
{
    public class FileViewAnalytics
    {
        public Ulid WorkSpaceId { get; set; }
        public Ulid FileId { get; set; }
        public Ulid ViewerId { get; set; }
        public int? ViewCount { get; set; }
        public TimeSpan? ViewTime { get; set; }
        public DateTime? ViewDate { get; set; }
    }
}
