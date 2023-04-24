using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.AspNetCore.Http;

namespace Domain.DTO
{
    public class AvatarDTO
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }
    }
}
