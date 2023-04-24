using System;
using System.Collections.Generic;
using System.Text;
using Domain.DTO;
using Domain.Entities;

//The implementation of this interface may contain methods 
//to retrieve and update the avatar images from a database or a file system. 
namespace ProfileData.Domain.Abstractions.Services
{
    public interface IAvatarService : IService<AvatarDTO>
    {
    }
}