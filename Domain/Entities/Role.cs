using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Role : IEntity<int>
    {
        [Key]
        public int ID { get; set; }
        public List<User> Users { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}

