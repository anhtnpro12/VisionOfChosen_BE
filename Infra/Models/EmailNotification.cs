using System.ComponentModel.DataAnnotations.Schema;

namespace VisionOfChosen_BE.Infra.Models
{
    [Table("email_notification")]
    public class EmailNotification : EmailNotificationProperties
    {
    }

    public class EmailNotificationProperties : ExtendModel
    {
        [Column("email")]
        public string Email { get; set; } = null!;
    }
}
