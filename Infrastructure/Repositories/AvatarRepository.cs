using System;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Application.Repositories;
using Application;

namespace Infrastructure.Repositories
{
    class AvatarRepository : Repository<Avatar>, IAvatarRepository
    {
        public AvatarRepository(ProfileDataContext _context) : base(_context)
        {

        }

    }
}
