using System;
using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
	public interface IUserRepository: IRepository<User>
    {
        User Get(Guid id);
        void Remove(Guid id);
    }
}

