﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Avatar : IEntity<Guid>
    {
        public Guid ID { get; set; }
        public byte[] Image { get; set; }
        public List<User> Users { get; set; }
    }


}