using System;
namespace Domain.DTO
{
    public class UserShortInfoDTO
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int RoleID { get; set; }
        public string Role { get; set; }
    }
}

