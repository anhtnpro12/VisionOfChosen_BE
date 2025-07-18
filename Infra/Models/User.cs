using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VisionOfChosen_BE.Infra.Consts;

namespace VisionOfChosen_BE.Infra.Models
{
    [Table("user")]
    public class User : UserProperties
    {
    }

    public class UserProperties : ExtendModel
    {
        [Required, EmailAddress]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; } = null!;
        [Column("role")]
        public string Role { get; set; } = RoleConst.User;
        [Column("name")]
        public string? Name { get; set; }
    }
}
