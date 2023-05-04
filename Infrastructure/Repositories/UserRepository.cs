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

        public User Get(Guid id)
        {
            return Context.Set<User>().Find(id);
        }

        public void Remove(Guid id)
        {
            if (Context.Set<User>() != null)
            {
                Context.Set<User>().Remove(Context.Set<User>().Find(id));
            }
        }
    }
}

