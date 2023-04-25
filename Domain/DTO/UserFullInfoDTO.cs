using System;
namespace Domain.DTO
{
    public class UserFullInfoDTO
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

#nullable enable
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }

#nullable enable
        public string? Description { get; set; }
#nullable enable
        public int? AvatarID { get; set; }
        public int RoleID { get; set; }

    }
}

