using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vstat_app.Analytics.Contracts.Models
{
    public class FileAnalytics
    {
        public Ulid Id { get; set; }
        public Ulid WorkSpaceId { get; set; }
        public Ulid OwnerId { get; set; }
        public string FileName { get; set; }
        public int TotalPagesCount { get; set; }
        public string? Type { get; set; }
        public string? Link { get; set; }
        [NotMapped]
        public ICollection<FileViewAnalytics>? FileViews { get; set; }
    }
}
