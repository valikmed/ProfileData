using System;
using Domain.Abstractions.Repositories;
using Domain.Entities;

namespace Application.Repositories
{

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ProfileDataContext _context) : base(_context)
        {
        }
    }
}

