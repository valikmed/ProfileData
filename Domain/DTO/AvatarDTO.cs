﻿using System;
using System.Text;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class AvatarDTO
    {
        public Guid ID { get; set; }
        public byte[] Image { get; set; }
    }
}
