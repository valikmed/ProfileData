using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Avatar : IEntity<int>
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public byte[] Image { get; set; }
        public List<User> Users { get; set; }

    }
}