using System;
using Domain.Abstractions.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ProfileDataContext _context) : base(_context)
        {
        }
    }
}

