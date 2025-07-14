using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisionOfChosen_BE.Infra.Models
{
    [Table("event")]
    [Comment("Bảng thống kê")]
    public class Event : EventProperties
    {
    }

    public class EventProperties : ExtendModel
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("changer")]
        public string? Changer { get; set; }

        [Column("service")]
        public string? Service { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("time")]
        public DateTime Time { get; set; }
    }
}
