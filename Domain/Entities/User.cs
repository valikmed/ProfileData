using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User : IEntity<Guid>
    {
        [Key]
        public Guid ID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
#nullable enable
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }

        [MaxLength(1024)]
#nullable enable
        public string Description { get; set; }

        [ForeignKey("Avatar")]
        public Guid? AvatarID { get; set; }
#nullable enable
        public Avatar? Avatar { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
#nullable disable
        public Role Role { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public static int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }
    }
}

