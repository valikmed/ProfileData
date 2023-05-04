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

        public Role Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            if (Context.Set<Role>() != null)
            {
                Context.Set<Role>().Remove(Context.Set<Role>().Find(id));
            }
        }
    }
}
