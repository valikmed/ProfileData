using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Role : IEntity<int>
    {
        public int ID { get; set; }
        public List<User> Users { get; set; }
        public string RoleName { get; set; }
    }
}

