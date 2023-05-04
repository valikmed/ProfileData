using System;
using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
	public interface IRoleRepository: IRepository<Role>
    {
        void Remove(int id);
        Role Get(int id);
    }
}

