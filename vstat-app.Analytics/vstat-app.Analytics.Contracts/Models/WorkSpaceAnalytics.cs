using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vstat_app.Analytics.Contracts.Models
{
    public class WorkSpaceAnalytics
    {
        public Ulid WorkSpaceId { get; set; }
        public Ulid OwnerId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IEnumerable<FileAnalytics>? Files { get; set; }
        [NotMapped]
        public ICollection<FileViewAnalytics>? FileViews { get; set; }
    }
}
