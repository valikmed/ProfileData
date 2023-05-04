using System;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Application.Repositories;
using Application;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    class AvatarRepository : Repository<Avatar>, IAvatarRepository
    {
        public AvatarRepository(ProfileDataContext _context) : base(_context)
        {
        }

        public Avatar Get(Guid id)
        {
            return Context.Set<Avatar>().Find(id);
        }

        public void Remove(Guid id)
        {
            if (Context.Set<Avatar>() != null)
            {
                Context.Set<Avatar>().Remove(Context.Set<Avatar>().Find(id));
            }
        }

    }
}
