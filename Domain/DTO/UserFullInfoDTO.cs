using System;
namespace Domain.DTO
{
    public class UserFullInfoDTO
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

#nullable enable
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }

#nullable enable
        public string Description { get; set; }
#nullable enable
        public Guid? AvatarID { get; set; }
        public int RoleID { get; set; }

        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }
}

