using System.ComponentModel.DataAnnotations.Schema;

namespace VisionOfChosen_BE.Infra.Models
{
    [Table("scan")]
    public class Scan : ScanProperties
    {
    }

    public class ScanProperties : ExtendModel
    {
        [Column("scan_time")]
        public DateTime ScanTime { get; set; }

        [Column("directory")]
        public string? Directory { get; set; }

        [Column("added_resources")]
        public int AddedResources { get; set; }

        [Column("changed_resources")]
        public int ChangedResources { get; set; }

        [Column("destroyed_resources")]
        public int DestroyedResources { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }
    }
}
